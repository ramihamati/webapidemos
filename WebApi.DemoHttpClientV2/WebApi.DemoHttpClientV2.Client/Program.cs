using Serilog;
using Serilog.Events;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace WebApi.DemoHttpClientV2.Client;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        CreateLogger();

        HttpClient httpClient = new(GetDelegatingHandler())
        {
            BaseAddress = new Uri(
                uriString: "https://127.0.0.1:6000/api/employees/",
                uriKind: UriKind.Absolute)
        };

        HttpRequestMessage message = new(
            method: HttpMethod.Get,
            requestUri: new Uri("employee", UriKind.Relative));

        message.Version = HttpVersion.Version20;
        var version = message.Version.ToString();

        HttpResponseMessage response = await httpClient.SendAsync(message);

        Console.WriteLine(response.StatusCode);
    }

    private static DelegatingHandler GetDelegatingHandler()
    {
        return new LoggingHandler(Log.Logger)
        {
            InnerHandler = GetHttpMessageHandler()
        };
    }

    private static HttpClientHandler GetHttpMessageHandler()
    {
        HttpClientHandler httpClientHandler = new()
        {
            //This should not be used in prod
            ServerCertificateCustomValidationCallback
                = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        httpClientHandler.ClientCertificates.Add(LoadCertificateFromStore());


        return httpClientHandler;
    }

    private static X509Certificate2 LoadCertificateFromStore()
    {
        using var store = new X509Store(
            StoreLocation.CurrentUser);

        store.Open(OpenFlags.ReadOnly);

        var certificate = store.Certificates.Find(
            X509FindType.FindByIssuerName,
            "localhost",
            validOnly: false);

        return certificate[0];
    }

    private static void CreateLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo
            .Console(restrictedToMinimumLevel: LogEventLevel.Debug)
            .CreateLogger();
    }
}
