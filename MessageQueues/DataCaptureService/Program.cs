using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using DataCaptureService.Services;

namespace DataCaptureService
{
    /*
         Implement Data capture service which will listen to a specific local folder 
         and retrieve documents of some specific format (i.e., PDF)
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = args[0] ?? Environment.CurrentDirectory + FileMessagesConsts.DefaultDirectory;
            var format = args[1] ?? FileMessagesConsts.DefaultFormat;

            if (!MonitorValidator.ValidateArgs(path, format, out var message))
            {
                throw new ArgumentException(message);
            }

            var producer = new ProducerBuilder<Null, string>(new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            }).Build();

            ICapturingProcessor processor = new CapturingProcessor(path, format, producer);

            var cancellationSource = new CancellationTokenSource();

            processor.MonitorFolderAsync(cancellationSource.Token);

            Console.WriteLine("Press \"q\" to exit");
            while (!cancellationSource.IsCancellationRequested)
            {
                if (Console.ReadLine()?.ToLower() == "q")
                {
                    cancellationSource.Cancel();
                }
            }
        }
    }
}