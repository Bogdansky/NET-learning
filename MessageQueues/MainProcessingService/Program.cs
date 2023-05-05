using System.Text.Json;
using MainProcessingService.Models;
using Confluent.Kafka;

namespace MainProcessingService
{
    internal class Program
    {
        private const string Topic = "file-messages";

        static void Main(string[] args)
        {
            var service = new FileProcessor(Topic);
            var cancellationSource = new CancellationTokenSource();

            service.ConsumeTopicMessagesAsync(cancellationSource.Token);

            while (true)
            {
                if (Console.ReadLine()?.ToLower() == "q")
                {
                    cancellationSource.Cancel();
                }
            }
        }
    }
}