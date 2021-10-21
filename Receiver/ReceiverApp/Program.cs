using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ReceiverApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Receiver App is running!");
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, collection) => 
                {
                    collection.AddHostedService<KafkaConsumerHostService>();
                });
    }

    public class KafkaConsumerHostService : IHostedService
    {
        private readonly ILogger<KafkaConsumerHostService> _logger;
        private readonly ClusterClient _cluster;

        public KafkaConsumerHostService(ILogger<KafkaConsumerHostService> logger)
        {
            _logger = logger;
            _cluster = new ClusterClient(new Configuration
            {
                Seeds = "localhost:9092"
            }, new ConsoleLogger());
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _cluster.ConsumeFromLatest("Kafka-Demo");
            _cluster.MessageReceived += record => 
            {
                _logger.LogInformation($"A message was received. {Encoding.UTF8.GetString(record.Value as byte[])}");
            };

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _cluster?.Dispose();
            await Task.CompletedTask;
        }
    }
}
