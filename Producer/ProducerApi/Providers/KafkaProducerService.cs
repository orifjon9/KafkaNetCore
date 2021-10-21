using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace ProducerApi.Providers
{
    public class KafkaProducerService : IProducerService
    {
        private readonly ILogger<KafkaProducerService> _logger;
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService(ILogger<KafkaProducerService> logger)
        {
            _logger = logger;
            _producer = new ProducerBuilder<Null, string>(new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            }).Build();
        }
        
        public async Task SendAsync(string data)
        {
            _logger.LogInformation($"A value: {data} has received");
            await _producer.ProduceAsync("Kafka-Demo", new Message<Null, string>
            {
                Value = data
            });
            _producer.Flush(TimeSpan.FromSeconds(5));
        }
    }
}