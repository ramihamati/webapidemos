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

        //HttpResponseMessage response1 = await RequestWithCertificateAndHttp1();
        HttpResponseMessage response2 = await RequestWithCertificateAndHttp2();
    }

    private static async Task<HttpResponseMessage> RequestWithCertificateAndHttp1()
    {
        HttpClient httpClient = new(GetDelegatingHandler())
        {
            BaseAddress = new Uri(
                uriString: "https://127.0.0.1:6000/api/employees/",
                uriKind: UriKind.Absolute)
        };

        HttpRequestMessage message = new(
            method: HttpMethod.Get,
            requestUri: new Uri("employee", UriKind.Relative));

        HttpResponseMessage response = await httpClient.SendAsync(message);
        return response;
    }


    private static async Task<HttpResponseMessage> RequestWithCertificateAndHttp2()
    {
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

        HttpResponseMessage response = await httpClient.SendAsync(message);
        return response;
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
        return new HttpClientHandler()
        {
            //This should not be used in prod
            ServerCertificateCustomValidationCallback
                = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
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
