using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Logic.Implementations;
using Airslip.Analytics.Services.ServiceBus.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Airslip.Analytics.Logic;

public static class Services
{
    public static IServiceCollection AddLogicServices(this IServiceCollection services)
    {
        services
            .AddScoped<IRelationshipService, RelationshipService>()
            .AddScoped<IBusinessService, BusinessService>()
            .AddScoped<IDashboardSnapshotService, DashboardSnapshotService>()
            .AddScoped<IRevenueAndRefundsService, RevenueAndRefundsService>()
            .AddScoped<IDebitsAndCreditsService, DebitsAndCreditsService>()
            .AddScoped(typeof(IRegisterDataService<,,>), typeof(RegisterDataService<,,>));

        return services;
    }
    
    public static IServiceCollection AddApiLogicServices(this IServiceCollection services)
    {
        services
            .AddScoped<IDashboardSnapshotService, DashboardSnapshotService>()
            .AddScoped<IRevenueAndRefundsService, RevenueAndRefundsService>()
            .AddScoped<IDebitsAndCreditsService, DebitsAndCreditsService>();

        return services;
    }
    
    public static IServiceCollection AddAnalyticsProcesses(this IServiceCollection services)
    {
        services
            .AddSingleton<IAnalysisMessagingService<BankAccountBalanceModel>, BalanceMessagingService>()
            .AddSingleton<IAnalysisMessagingService<MerchantTransactionModel>, CommerceMessagingService>()
            .AddSingleton<IAnalysisMessagingService<BankTransactionModel>, TransactionsMessagingService>()
            
            .AddScoped(typeof(IAnalysisHandlingService<>), typeof(AnalysisHandlingService<>))
            
            // Order here is important
            .AddScoped<IAnalyticsProcess<BankAccountBalanceModel>, CreateAccountBalanceSnapshot>() // Per record
            .AddScoped<IAnalyticsProcess<BankAccountBalanceModel>, GenerateBusinessBalanceSnapshot>() // Per record
            .AddScoped<IAnalyticsProcess<BankAccountBalanceModel>, UpdateAccountBalanceSummary>() // Per record
            .AddScoped<IAnalyticsProcess<BankAccountBalanceModel>, UpdateBusinessBalanceSummary>() // Per record
            
            .AddScoped<IAnalyticsProcess<MerchantTransactionModel>, CreateMerchantMetricSnapshot>() // Per record
            .AddScoped<IAnalyticsProcess<MerchantTransactionModel>, CreateMerchantAccountMetricSnapshot>() // Per record
            .AddScoped<IAnalyticsProcess<BankTransactionModel>, CreateBankAccountMetricSnapshot>(); // Per record

        return services;
    }
}