using System.Text.Json;
using Confluent.Kafka;
using Infrastructure.Enums;
using Infrastructure.Models;

namespace MainProcessingService.Services
{
    internal class FileProcessor : IFileProcessor
    {
        private readonly string _topic;
        private readonly Dictionary<string, long> _files;
        private readonly string _fileStorageDirectory;

        public FileProcessor(string topic)
        {
            _topic = topic;
            _files = new Dictionary<string, long>();
            _fileStorageDirectory = Environment.CurrentDirectory + "/files";
        }

        public Task ConsumeTopicMessagesAsync(CancellationToken token)
        {
            try
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "file-consumer-1",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

                consumer.Subscribe(_topic);

                return Task.Factory.StartNew((obj) =>
                {
                    ConsumeMessages(obj as IConsumer<Ignore, string>, token);
                }, consumer, token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        private void ConsumeMessages(IConsumer<Ignore, string> consumer, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(TimeSpan.FromMilliseconds(10));

                    if (result is not null && !string.IsNullOrEmpty(result.Message?.Value))
                    {
                        var kafkaMessage = JsonSerializer.Deserialize<KafkaMessage>(result.Message.Value);

                        switch (kafkaMessage?.Type)
                        {
                            case MessageTypesEnum.FileCreated:
                                HandleFileCreatedMessage(kafkaMessage.Message);
                                break;
                            case MessageTypesEnum.FileTranser:
                                HandleFileTransfertMessage(kafkaMessage.Message);
                                break;
                            default: throw new Exception("Unexpected message type");
                        };
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        private void HandleFileCreatedMessage(string message)
        {
            if (message is null)
            {
                return;
            }

            var fileMessage = JsonSerializer.Deserialize<FileCreatedMessage>(message);

            if (fileMessage is null)
            {
                throw new Exception("Message should contain non-null value!");
            }

            _files[fileMessage.Name] = fileMessage.ChunksNumber;

            var path = Path.Combine(_fileStorageDirectory, fileMessage.Name);

            File.Create(path).Close();

            Console.WriteLine("File {0} was created! Path {1}", fileMessage?.Name, path);
        }

        private void HandleFileTransfertMessage(string message)
        {
            if (message is null)
            {
                return;
            }

            var fileMessage = JsonSerializer.Deserialize<FileTransferMessage>(message);

            if (fileMessage is null)
            {
                throw new Exception("Message should contain non-null value!");
            }

            if (!_files.TryGetValue(fileMessage.Name, out _))
            {
                throw new Exception("File was not created!");
            }

            using (var fileStream = new FileStream(GetPath(fileMessage.Name), FileMode.Append))
            {
                fileStream.Write(fileMessage.Data, 0, fileMessage.Data.Length);
            }

            Console.WriteLine("Position {0}. {1} remain", fileMessage.Position, _files[fileMessage.Name] - fileMessage.Position);
        }

        private string GetPath(string name) => Path.Combine(_fileStorageDirectory, name);
    }
}
