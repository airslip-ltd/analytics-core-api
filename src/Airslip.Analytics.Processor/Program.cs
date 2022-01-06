using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Services.SqlServer;
using Airslip.Common.Auth.Functions.Extensions;
using Airslip.Common.Auth.Functions.Middleware;
using Airslip.Common.Functions.Extensions;
using Airslip.Common.Repository.Enums;
using Airslip.Common.Repository.Extensions;
using Airslip.Common.Repository.Implementations;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Services.AutoMapper;
using Airslip.Common.Services.AutoMapper.Extensions;
using Airslip.Common.Services.FluentValidation;
using Airslip.Common.Services.SqlServer;
using Airslip.Common.Services.SqlServer.Implementations;
using Airslip.Common.Types.Configuration;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Airslip.Analytics.Processor;

internal class Program
{
    public static void Main(string[] args)
    {
        IHost host = BuildWebHost(args);

        using (IServiceScope scope = host.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;
            SqlServerContext context = services.GetRequiredService<SqlServerContext>();
            context.Database.EnsureCreated();
            // DbInitializer.Initialize(context);
        }
        
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
                    .AddAirslipFunctionAuth(context.Configuration)
                    .AddRepositories(RepositoryUserType.Service)
                    .AddSingleton<IModelValidator<MyModel>, NullValidator<MyModel>>()
                    .AddAutoMapper(cfg =>
                    {
                        cfg.CreateMap<MyEntity, MyModel>().ReverseMap();
                    }, MapperUsageType.Service);

                services
                    .AddAirslipSqlServer<SqlServerContext>(context.Configuration)
                    .AddScoped(typeof(IEntitySearch<,>), typeof(EntitySearch<,>));
            })
            .Build();
        
        return host;
    }
}