using Airslip.Analytics.Core.Constants;
using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Implementations;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw;
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
                        cfg.CreateMap<AccountBalanceModel, AccountBalance>().ReverseMap();
                        cfg.CreateMap<AccountBalanceDetailModel, AccountBalanceDetail>().ReverseMap();
                        cfg.CreateMap<AccountBalanceCreditLineModel, AccountBalanceCreditLine>().ReverseMap();
                        cfg.CreateMap<AccountModel, Account>().ReverseMap();
                        cfg.CreateMap<BankModel, Bank>().ReverseMap();
                        cfg.CreateMap<TransactionModel, Transaction>().ReverseMap();
                        cfg.CreateMap<BankCountryCodeModel, BankCountryCode>().ReverseMap();
                        
                    }, MapperUsageType.Service);

                services
                    .AddAirslipSqlServer<SqlServerContext>(context.Configuration)
                    .AddScoped(typeof(IRegisterDataService<,,>), typeof(RegisterDataService<,,>))
                    .AddScoped(typeof(IEntitySearch<,>), typeof(EntitySearch<,>));

                services.UseMessageHandoff(handoff =>
                {
                    handoff.Register<IRegisterDataService<Bank, BankModel, RawYapilyBankModel>>(Constants.EVENT_QUEUE_YAPILY_BANKS);
                    handoff.Register<IRegisterDataService<Account, AccountModel, RawYapilyAccountModel>>(Constants.EVENT_QUEUE_YAPILY_ACCOUNTS);
                    handoff.Register<IRegisterDataService<Transaction, TransactionModel, RawYapilyTransactionModel>>(Constants.EVENT_QUEUE_YAPILY_TRANSACTIONS);
                    handoff.Register<IRegisterDataService<AccountBalance, AccountBalanceModel, RawYapilyBalanceModel>>(Constants.EVENT_QUEUE_YAPILY_BALANCES);
                    handoff.Register<IRegisterDataService<SyncRequest, SyncRequestModel, RawYapilySyncRequestModel>>(Constants.EVENT_QUEUE_YAPILY_SYNC_REQUESTS);
                });

            })
            .Build();
        
        return host;
    }
}