using Airslip.Common.Auth.Functions.Extensions;
using Airslip.Common.Auth.Functions.Middleware;
using Airslip.Common.Functions.Extensions;
using Airslip.Common.Types.Configuration;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Airslip.Bootstrap.Function;

internal class Program
{
    public static void Main(string[] args)
    {
        var host = BuildWebHost(args);

        host.RunAsync()
            .Wait();
    }

    private static IHost BuildWebHost(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults(worker =>
            {
                worker.UseNewtonsoftJson();
                worker.UseMiddleware<ApiKeyAuthenticationMiddleware>();
                worker.UseMiddleware<ApiKeyAuthorisationMiddleware>();
            })
            .ConfigureOpenApi()
            .ConfigureServices(services =>
            {
                IConfiguration config = new ConfigurationBuilder()
                    .AddDefaultConfig(args)
                    .Build();

                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .CreateLogger();

                // Add HttpClient
                services.AddHttpClient();

                // Add Options
                services
                    .AddOptions()
                    .Configure<EnvironmentSettings>(config.GetSection(nameof(EnvironmentSettings)))
                    .Configure<PublicApiSettings>(config.GetSection(nameof(PublicApiSettings)))
                    .Configure<EventHubSettings>(config.GetSection(nameof(EventHubSettings)));
                    
                services
                    .AddAirslipFunctionAuth(config);

                services.AddSingleton<ILogger>(_ => logger);
            })
            .Build();
        
        return host;
    }
}