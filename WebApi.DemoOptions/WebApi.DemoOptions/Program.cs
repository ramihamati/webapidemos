using Microsoft.Extensions.Options;
using WebApi.DemoOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services
    .Configure<OptionMongoDb>(builder.Configuration.GetRequiredSection("Mongo"));

builder.Services
    .AddSingleton<IConfigureOptions<OptionRedis>, OptionRedisConfigure>();
builder.Services
    .AddSingleton<IValidateOptions<OptionRedis>, ValidateOptionsRedis>();

builder.Services.AddOptions<OptionsKafka>()
    .BindConfiguration("Kafka")
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

app.MapControllers();

app.Run();
