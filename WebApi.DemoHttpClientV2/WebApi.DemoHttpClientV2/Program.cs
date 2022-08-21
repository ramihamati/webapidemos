using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace WebApi.DemoHttpClientV2;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options =>
                {
                    IPAddress iPAddress = IPAddress.Parse("127.0.0.1");

                    options.Listen(iPAddress, 6000, lOptions =>
                    {
                        lOptions.UseHttps();
                        lOptions.Protocols = HttpProtocols.Http2;
                    });
                });
                webBuilder.UseStartup<Startup>();
            });
}
