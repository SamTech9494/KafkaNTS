using Confluent.Kafka;

namespace KafkaProject.Service;

public class ConsumerService : IConsumerService
{
    private readonly IConsumer<Null,string> _consumer;
    const string Topic = "demo";
    public ConsumerService()
    {
        var consumerConfig = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9092"
        };

        _consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
        _consumer.Subscribe(Topic);
    }
    public void Consume()
    {
        Console.WriteLine("In consume service");
        var consumeResult = _consumer.Consume(CancellationToken.None);
        try
        {
            Console.WriteLine("consumer in try");
            while (true)
            {
                Console.WriteLine(
                    $"Consumed event from topic {Topic} with key {consumeResult.Message.Key,-10} and value {consumeResult.Message.Value}");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("OperationCanceledException");
            // Ctrl-C was pressed.
        }
        finally
        {
            Console.WriteLine("consumer close");
            _consumer.Close();
        }
    }
}