using Confluent.Kafka;
using KafkaProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProject.Controllers;

[ApiController]
[Route("[controller]")]
public class ProducerController : ControllerBase
{
    private readonly ILogger<ProducerController> _logger;
    private readonly IProducerService _producerService;
    private IProducer<Null, string>? _producer;

    public ProducerController(ILogger<ProducerController> logger, IProducerService producerService)
    {
        _logger = logger;
        _producerService = producerService;
        var producerConfig = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092"
        };

        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    [HttpGet(Name = "ProduceV2")]
    public async Task ProduceV2()
    {
        await _producerService.StartAsync(CancellationToken.None);
        await _producerService.StopAsync(CancellationToken.None);
    }
    
    // [HttpGet(Name = "ProduceV1")]
    // public async Task Produce()
    // {
    //     const string topic = "purchases";
    //
    //
    //     string[] users = { "eabara", "jsmith", "sgarcia", "jbernard", "htanaka", "awalther" };
    //     string[] items = { "book", "alarm clock", "t-shirts", "gift card", "batteries" };
    //
    //     var numProduced = 0;
    //     var rnd = new Random();
    //     const int numMessages = 10;
    //     for (var i = 0; i < numMessages; ++i)
    //     {
    //         var user = users[rnd.Next(users.Length)];
    //         var item = items[rnd.Next(items.Length)];
    //
    //         Console.WriteLine(item);
    //
    //         await _producer.ProduceAsync(topic, new Message<Null, string>()
    //         {
    //             Value = item
    //         });
    //         // using (var producer = new ProducerBuilder<Null, string>(producerConfig).Build())
    //         // {
    //         // var numProduced = 0;
    //         // var rnd = new Random();
    //         // const int numMessages = 10;
    //         // for (var i = 0; i < numMessages; ++i)
    //         // {
    //         //     var user = users[rnd.Next(users.Length)];
    //         //     var item = items[rnd.Next(items.Length)];
    //         //
    //         //     await producer.ProduceAsync("demo", new Message<Null, string>()
    //         //     {
    //         //         Value = item
    //         //     });
    //
    //         //---
    //
    //
    //         // producer.ProduceAsync(topic, new Message<string, string> { Key = user, Value = item },
    //         //     (deliveryReport) =>
    //         //     {
    //         //         if (deliveryReport.Error.Code != ErrorCode.NoError)
    //         //         {
    //         //             Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
    //         //         }
    //         //         else
    //         //         {
    //         //             Console.WriteLine($"Produced event to topic {topic}: key = {user,-10} value = {item}");
    //         //             numProduced += 1;
    //         //         }
    //         //     });
    //         // }
    //
    //         // producer.Flush(TimeSpan.FromSeconds(10));
    //         Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
    //     }
  
   
}