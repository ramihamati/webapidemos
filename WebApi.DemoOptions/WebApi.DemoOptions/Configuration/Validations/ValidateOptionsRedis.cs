using Microsoft.Extensions.Options;

namespace WebApi.DemoOptions;

public class ValidateOptionsRedis : IValidateOptions<OptionRedis>
{
    public ValidateOptionsResult Validate(string name, OptionRedis options)
    {
        if (options.Host == "localhost")
        {
            return ValidateOptionsResult.Fail("Localhost is not a valid production host");
        }

        return ValidateOptionsResult.Success;
    }
}
