using Serilog;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.DemoHttpClientV2.Client;

public class LoggingHandler : DelegatingHandler
{
    private readonly ILogger _logger;

    public LoggingHandler(ILogger logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _logger.Information($"Request: {request}");
        try
        {
            // base.SendAsync calls the inner handler
            var response = await base.SendAsync(request, cancellationToken);
            _logger.Information($"Response: {response}");
            return response;
        }
        catch (Exception ex)
        {
            _logger.Information($"Failed to get response: {ex}");
            throw;
        }
    }
}
