using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Implementations;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Processor.Mappers;
using Airslip.Analytics.Services.SqlServer;
using Airslip.Common.Auth.Functions.Extensions;
using Airslip.Common.Functions.Extensions;
using Airslip.Common.Repository.Enums;
using Airslip.Common.Repository.Extensions;
using Airslip.Common.Repository.Implementations;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Services.AutoMapper;
using Airslip.Common.Services.AutoMapper.Extensions;
using Airslip.Common.Services.FluentValidation;
using Airslip.Common.Services.Handoff;
using Airslip.Common.Services.Handoff.Extensions;
using Airslip.Common.Services.SqlServer;
using Airslip.Common.Types.Configuration;
using Microsoft.EntityFrameworkCore;
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
            context.Database.Migrate();
            // DbInitializer.Initialize(context);
        }
        
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
                    .AddAirslipFunctionAuth(context.Configuration)
                    .AddRepositories(RepositoryUserType.Service)
                    .AddSingleton(typeof(IModelValidator<>), typeof(NullValidator<>))
                    .AddAutoMapper(cfg =>
                    {
                        cfg.AddRawYapilyData();
                        cfg.CreateMap<AccountModel, Account>().ReverseMap();
                        cfg.CreateMap<BankModel, Bank>().ReverseMap();
                        cfg.CreateMap<BankCountryCodeModel, BankCountryCode>().ReverseMap();
                    }, MapperUsageType.Service);

                services
                    .AddAirslipSqlServer<SqlServerContext>(context.Configuration)
                    .AddScoped(typeof(IRegisterDataService<,,>), typeof(RegisterDataService<,,>))
                    .AddScoped(typeof(IEntitySearch<,>), typeof(EntitySearch<,>));

                services.UseMessageHandoff(handoff =>
                {
                    handoff.Register<IRegisterDataService<Bank, BankModel, RawBankModel>>("yapily-banks");
                    handoff.Register<IRegisterDataService<Account, AccountModel, RawAccountModel>>("yapily-accounts");
                });

            })
            .Build();
        
        return host;
    }
}