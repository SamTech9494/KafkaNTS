using Confluent.Kafka;
using KafkaProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProject.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsumerController : ControllerBase
{
    private readonly ILogger<ConsumerController> _logger;
    private readonly IConsumerService _consumerService;

    public ConsumerController(ILogger<ConsumerController> logger, IConsumerService consumerService)
    {
        _logger = logger;
        _consumerService = consumerService;
    }

    [HttpGet(Name = "Consume")]
    public void Consume()
    {

        Console.WriteLine("Consume loh");
        
        _consumerService.Consume();
    }
}