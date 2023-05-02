using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using DataCaptureService.Models;

namespace DataCaptureService
{
    /*
     Implement Data capture service which will listen to a specific local folder 
     and retrieve documents of some specific format (i.e., PDF)
     */
    internal class Program
    {
        private const string DefaultDirectory = "\\files";
        private const string DefaultFormat = "pdf";

        private static IProducer<Null, string> _producer; 

        static void Main(string[] args)
        {
            var path = args[0] ?? Environment.CurrentDirectory + DefaultDirectory;
            var format = args[1] ?? DefaultFormat;

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
            var message = JsonSerializer.Serialize(new FileCreatedMessage(e.Name));
            var kafkaMessage = new Message<Null, string> { Value = message };

            _producer.Produce("file-messages", kafkaMessage);
        }
    }
}