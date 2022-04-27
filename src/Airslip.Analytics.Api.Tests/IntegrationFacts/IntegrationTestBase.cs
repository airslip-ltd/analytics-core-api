using Airslip.Common.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Serilog;
using System;
using System.Net.Http;

namespace Airslip.Analytics.Api.Tests.IntegrationFacts;

public class IntegrationTestBase
{
    private static readonly TransactionsApiWebApplicationFactory<Program> TransactionsFactory;
    protected readonly Uri BaseUri = new("http://localhost/v1/");
    protected const string HeartbeatEndpoint = "heartbeat";
    private static readonly Mock<ILogger> _mockLogger = new();

    static IntegrationTestBase()
    {
        TransactionsFactory = new TransactionsApiWebApplicationFactory<Program>(_mockLogger.Object);

        TransactionsFactory.WithWebHostBuilder(builder => { builder.UseSerilog(_mockLogger.Object); });
    }

    protected static HttpClient GetUnauthorizedHttpClient()
    {
        HttpClient client = TransactionsFactory.CreateClient();
        client.DefaultRequestHeaders.Add("Accept", Json.MediaType);
        return client;
    }
}

public class TransactionsApiWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly ILogger _logger;

    public TransactionsApiWebApplicationFactory(ILogger logger)
    {
        _logger = logger;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(_logger);
        });
    }
}