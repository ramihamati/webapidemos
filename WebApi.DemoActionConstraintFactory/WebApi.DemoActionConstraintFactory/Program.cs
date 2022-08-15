using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using WebApi.DemoActionConstraintFactory.Routing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(options =>
{
    var formatter = options.InputFormatters
        .OfType<NewtonsoftJsonInputFormatter>()
        .First();

    formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(InputAcceptTypes.VndMyAppAccounting));
    formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(InputAcceptTypes.VndMyAppHR));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
