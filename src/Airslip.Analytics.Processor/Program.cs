using Airslip.Analytics.Logic;
using Airslip.Analytics.Processor.Extensions;
using Airslip.Analytics.Services.SqlServer;
using Airslip.Common.Auth.Functions.Extensions;
using Airslip.Common.Functions.Extensions;
using Airslip.Common.Repository.Enums;
using Airslip.Common.Repository.Extensions;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Services.AutoMapper;
using Airslip.Common.Services.AutoMapper.Extensions;
using Airslip.Common.Services.FluentValidation;
using Airslip.Common.Services.Handoff;
using Airslip.Common.Services.SqlServer;
using Airslip.Common.Types.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Airslip.Analytics.Processor;

internal class Program
{
    public static void Main(string[] args)
    {
        IHost host = BuildWebHost(args);
   
        host.RunAsync()
            .Wait();
    }

    private static IHost BuildWebHost(string[] args)
    {
        IHost host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureHostConfiguration(builder =>
            {
                builder.AddDefaultConfig(args);
            })
            .UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            })
            .ConfigureServices((context, services) =>
            {
                // Add HttpClient
                services.AddHttpClient();

                // Add Options
                services
                    .AddOptions()
                    .Configure<EnvironmentSettings>(context.Configuration.GetSection(nameof(EnvironmentSettings)))
                    .Configure<PublicApiSettings>(context.Configuration.GetSection(nameof(PublicApiSettings)))
                    .Configure<EventHubSettings>(context.Configuration.GetSection(nameof(EventHubSettings)));

                services
                    .AddRepositories(RepositoryUserType.Service)
                    .AddEntitySearch()
                    .AddSingleton(typeof(IModelValidator<>), typeof(NullValidator<>))
                    .AddAutoMapper(ServiceRegistration.RegisterMappings, MapperUsageType.Service)
                    .AddAirslipFunctionAuth(context.Configuration);

                services
                    .AddAirslipSqlServer<SqlServerContext>(context.Configuration)
                    .AddAnalyticsProcesses()
                    .AddLogicServices();

                services.UseMessageHandoff(ServiceRegistration.RegisterHandoff);
                
            })
            .Build();
        
        return host;
    }
}