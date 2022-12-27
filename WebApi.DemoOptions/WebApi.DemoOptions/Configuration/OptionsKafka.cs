using System.ComponentModel.DataAnnotations;

namespace WebApi.DemoOptions;

public class OptionsKafka
{
    [Required]
    [MinLength(1)]
    public string? Host { get; set; }
    public int Port { get; set; }
}
