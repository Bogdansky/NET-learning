using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Infrastructure.Enums;
using Infrastructure.Models;

namespace DataCaptureService
{
    internal class CapturingProcessor
    {
        private readonly string _path;
        private readonly string _format;
        private readonly IProducer<Null, string> _producer;
        private readonly Dictionary<string, bool> _filesUploaded;
        private CancellationToken _token;

        public CapturingProcessor(string path, string format, IProducer<Null, string> producer)
        {
            _path = path;
            _format = format;
            _producer = producer;

            _filesUploaded = new Dictionary<string, bool>();
        }

        public Task MonitorFolderAsync(CancellationToken token)
        {
            _token = token;

            return Task.Factory.StartNew((state) =>
            {
                (var format, var path) = state as FileCapturedModel;

                var watcher = new FileSystemWatcher(path)
                {
                    Filter = $"*.{format}"
                };

                // to track the large copied files
                watcher.EnableRaisingEvents = true;

                watcher.Created += OnCreated;
            }, new FileCapturedModel(_format, _path), _token);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("{0} event captured. File name is {1}", e.ChangeType, e.Name);
            try
            {
                FileInfo fileInfo;

                while (!IsFileAccessible(e.FullPath, out fileInfo))
                {
                    Thread.Sleep(100);
                }

                var firstMessage = GetFileCreatedMessage(e.Name, fileInfo.Length);

                _producer.Produce(FileMessagesConsts.Topic, firstMessage, OnDeliveryHandler);

                var stream = fileInfo.OpenRead();

                while (SendPackage(stream, e.Name))
                {
                    _token.ThrowIfCancellationRequested();
                }

                stream.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was arisen exception during OnCreated handling. Message is {0}", ex.Message);
            }
        }

        private bool IsFileAccessible(string path, out FileInfo fileInfo)
        {
            fileInfo = new FileInfo(path);

            try
            {
                var stream = fileInfo.OpenRead();
                stream.Dispose();

                return true;
            }
            catch(IOException)
            {
                return false;
            }
        }

        private Message<Null, string> GetFileCreatedMessage(string name, long fileSize)
        {
            var message = new FileCreatedMessage(name, MessagesHelper.GetChunksNumber(fileSize, FileMessagesConsts.DataTransferSizeInBytes));
            var kafkaMessage = new KafkaMessage(MessageTypesEnum.FileCreated, JsonSerializer.Serialize(message));

            var fileCreatedMessage = new Message<Null, string> { Value = JsonSerializer.Serialize(kafkaMessage) };

            return fileCreatedMessage;
        }

        private bool SendPackage(FileStream stream, string name)
        {
            var buffer = new byte[FileMessagesConsts.DataTransferSizeInBytes];
            var bytesNumber = stream.Read(buffer, 0, buffer.Length);

            if (bytesNumber == 0) 
            {
                return false;
            }

            var position = Convert.ToInt64(Math.Ceiling(stream.Position / (decimal)bytesNumber));

            var fileTransferMessage = new FileTransferMessage(name, position, buffer);
            var kafkaMessage = new KafkaMessage(MessageTypesEnum.FileTranser, JsonSerializer.Serialize(fileTransferMessage));

            _producer.Produce(
                FileMessagesConsts.Topic,
                new Message<Null, string> { Value = JsonSerializer.Serialize(kafkaMessage) },
                OnDeliveryHandler);

            Console.WriteLine("{0} bytes was read. Position {1}.", bytesNumber, stream.Position);

            return true;
        }

        private static void OnDeliveryHandler(DeliveryReport<Null, string> obj)
        {
            Console.WriteLine("On-delivery handler is called with status {0}", obj.Status);
            Console.WriteLine("Whole object: {0}", JsonSerializer.Serialize(obj));
        }
    }
}
