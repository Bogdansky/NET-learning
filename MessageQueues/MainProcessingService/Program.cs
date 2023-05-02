using System.Text.Json;
using System.Threading;
using Confluent.Kafka;
using MainProcessingService.Models;

namespace MainProcessingService
{
    internal class Program
    {
        private const string Topic = "file-messages";

        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "file-consumer",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe(Topic);

            var cancellationSource = new CancellationTokenSource();

            var task = Task.Factory.StartNew((obj) =>
            {
                ConsumeMessages(obj as IConsumer<Ignore, string>, cancellationSource.Token);
            }, consumer, cancellationSource.Token);
        }

        private static void ConsumeMessages(IConsumer<Ignore, string> consumer, CancellationToken cancellationToken)
        {
            while(true)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                var result = consumer.Consume(TimeSpan.FromMilliseconds(10));
                HandleMessage(result.Message.Value);
            }
        }

        private static void HandleMessage(string message)
        {
            if (message is null)
            {
                return;
            }

            var fileMessage = JsonSerializer.Deserialize<FileCreatedMessage>(message);

            Console.WriteLine(fileMessage?.Name);
        }
    }
}