using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Infrastructure.Models;

namespace MainProcessingService
{
    internal class FileProcessor
    {
        private readonly string _topic;

        public FileProcessor(string topic)
        {
            _topic = topic;
        }

        public Task ConsumeTopicMessagesAsync(CancellationToken token)
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

        private static void ConsumeMessages(IConsumer<Ignore, string> consumer, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = consumer.Consume(TimeSpan.FromMilliseconds(10));
                if (result is not null)
                {
                    HandleMessage(result.Message.Value);
                }
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
