using Confluent.Kafka;
using KafkaProject.Service;

namespace KafkaNTS.Service;

public class ConsumerService : IConsumerService
{
    private readonly ILogger<ConsumerService> _logger;
    private readonly IConsumer<Null,string> _consumer;
    const string Topic = "demo";
    public ConsumerService(ILogger<ConsumerService> logger) 
    {
        _logger = logger;
        var consumerConfig = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9092"
        };

        _consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
        _consumer.Subscribe(Topic);
    }
    public void Consume()
    {
        _logger.LogInformation("In consume service");
        var consumeResult = _consumer.Consume(CancellationToken.None);
        try
        {
            _logger.LogInformation("consumer in try");
            while (true)
            {
                _logger.LogInformation("Consumed event from topic {Topic} with key {@MessageKey} and value {MessageValue}", Topic, consumeResult.Message.Key, consumeResult.Message.Value);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("OperationCanceledException");
            // Ctrl-C was pressed.
        }
        finally
        {
            Console.WriteLine("consumer close");
            _consumer.Close();
        }
    }
}