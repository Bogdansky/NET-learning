using System.Text.Json;
using Confluent.Kafka;
using MainProcessingService.Services;

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

            Console.WriteLine("Press \"q\" to exit");
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