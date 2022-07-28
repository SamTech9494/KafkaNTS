using Confluent.Kafka;
using KafkaProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace KafkaNTS.Controllers;

[ApiController]
[Route("[controller]")]
public class ProducerController : ControllerBase
{
    private readonly ILogger<ProducerController> _logger;
    private readonly IProducerService _producerService;

    public ProducerController(ILogger<ProducerController> logger, IProducerService producerService)
    {
        _logger = logger;
        _producerService = producerService;
        var producerConfig = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092"
        };

        new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    [HttpGet(Name = "ProduceV2")]
    public async Task ProduceV2()
    {
        await _producerService.StartAsync(CancellationToken.None);
        await _producerService.StopAsync(CancellationToken.None);
    }
}