using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using Infrastructure.Enums;
using Infrastructure.Models;

namespace DataCaptureService
{
    /*
         Implement Data capture service which will listen to a specific local folder 
         and retrieve documents of some specific format (i.e., PDF)
     */
    internal class Program
    {
        private static IProducer<Null, string> _producer; 

        static void Main(string[] args)
        {
            var path = args[0] ?? Environment.CurrentDirectory + FileMessagesConsts.DefaultDirectory;
            var format = args[1] ?? FileMessagesConsts.DefaultFormat;

            if(!MonitorValidator.ValidateArgs(path, format, out var message))
            {
                throw new ArgumentException(message);
            }

            _producer = new ProducerBuilder<Null, string>(new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            }).Build();

            MonitorFolderAsync(format, path);

            while(true)
            {
                Console.ReadLine();
                _producer.Dispose();
            }  
        }

        private static Task MonitorFolderAsync(string format, string path)
        {
            return Task.Factory.StartNew((state) =>
            {
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                (var format, var path) = state as FileCapturedModel;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

                var watcher = new FileSystemWatcher(path)
                {
                    Filter = $"*.{format}"
                };

                watcher.EnableRaisingEvents = true;

                watcher.Created += new FileSystemEventHandler(OnCreated);
            }, new FileCapturedModel(format, path));
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            var firstMessage = GetFileCreatedMessage(e.FullPath, e.Name);

            try
            {
                _producer.Produce(FileMessagesConsts.Topic, firstMessage, OnDeliveryHandler);

                using var stream = new FileStream(e.FullPath, FileMode.Open);

                while (true)
                {
                    var buffer = new byte[FileMessagesConsts.DataTransferSizeInBytes];
                    var bytesNumber = stream.Read(buffer, 0, buffer.Length);

                    if (bytesNumber == 0)
                    {
                        break;
                    }

                    var fileTransferMessage = new FileTransferMessage(e.Name, stream.Position, buffer);
                    var kafkaMessage = new KafkaMessage(MessageTypesEnum.FileTranser, JsonSerializer.Serialize(fileTransferMessage));

                    _producer.Produce(
                        FileMessagesConsts.Topic,
                        new Message<Null, string> { Value = JsonSerializer.Serialize(kafkaMessage) },
                        OnDeliveryHandler);

                    Console.WriteLine("{0} bytes was read. Position {1}.", bytesNumber, stream.Position);
                }
            }
            catch
            {
                Console.WriteLine("There was arisen exception during OnCreated handling");
            }
        }

        private static Message<Null, string> GetFileCreatedMessage(string path, string name)
        {
            var fileInfo = new FileInfo(path);

            var fileCreatedMessage = new FileCreatedMessage(name, MessagesHelper.GetChunksNumber(fileInfo.Length, FileMessagesConsts.DataTransferSizeInBytes));
            var message = new KafkaMessage(MessageTypesEnum.FileCreated, JsonSerializer.Serialize(fileCreatedMessage));

            return new Message<Null, string> { Value = JsonSerializer.Serialize(message) };
        }

        private static void OnDeliveryHandler(DeliveryReport<Null, string> obj)
        {
            Console.WriteLine("On-delivery handler is called with status {0}", obj.Status);
            Console.WriteLine("Whole object: {0}", JsonSerializer.Serialize(obj));
        }
    }
}