using Confluent.Kafka;
using KafkaProject.Service;

namespace KafkaNTS.Service;

public class ProducerService : IProducerService
{
    private readonly ILogger<ProducerService> _logger;
    private readonly IProducer<Null,string> _producer;

    public ProducerService(ILogger<ProducerService> logger)
    {
        _logger = logger;
        var producerConfig = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092",
        };
        _producer = new ProducerBuilder<Null,string>(producerConfig).Build();
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 100; i++)
        {
            var value = $"Hello W1 {i}";
            _logger.LogInformation(value);
            await _producer.ProduceAsync("demo", new Message<Null, string>()
            {
                Value = value
            }, cancellationToken);
            
            _producer.Flush(TimeSpan.FromSeconds(10));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _producer?.Dispose();
        return Task.CompletedTask;
    }
}