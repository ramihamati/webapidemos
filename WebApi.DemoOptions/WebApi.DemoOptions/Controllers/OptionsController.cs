using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.DemoOptions.Controllers;

[ApiController]
[Route("api/options")]

public class OptionsController : ControllerBase
{
    private readonly IOptions<OptionMongoDb> _mongoOptions;
    private readonly IOptions<OptionRedis> _redisOptions;
    private readonly IOptions<OptionsKafka> _kafkaOptions;

    public OptionsController(
        IOptions<OptionMongoDb> mongoOptions,
        IOptions<OptionRedis> redisOptions,
        IOptions<OptionsKafka> kafkaOptions)
    {
        _mongoOptions = mongoOptions;
        _redisOptions = redisOptions;
        _kafkaOptions = kafkaOptions;
    }

    [HttpGet]
    public IActionResult Get()
    {
        // calling a redisOption property will trigger the validation on the option model
        var hostRedis = _redisOptions.Value.Host;
        var hostKafka = _kafkaOptions.Value.Host;

        return Ok();
    }
}
