using KafkaProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace KafkaNTS.Controllers;

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
        
        _logger.LogInformation("Consume loh");

        _consumerService.Consume();
    }
}