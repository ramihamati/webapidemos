using Microsoft.Extensions.Options;

namespace WebApi.DemoOptions;

public class OptionRedis
{
    public string? Host { get; set; }
    public int Port { get; set; }
}

public class OptionRedisConfigure : IConfigureOptions<OptionRedis>
{
    private readonly IConfiguration _configuration;

    public OptionRedisConfigure(
        IConfiguration configuration)
    {
        _configuration = configuration.GetRequiredSection("Redis");
    }

    public void Configure(OptionRedis options)
    {
        options.Host = _configuration.GetValue<string>("Host")
            ?? throw new Exception("The host value is not provided");
        options.Port = _configuration.GetValue<int?>("Port")
            ?? throw new Exception("The port value is not provided");
    }
}
