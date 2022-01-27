using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Services.SqlServer.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Airslip.Analytics.Services.SqlServer.Extensions;

public static class Services
{
    public static IServiceCollection AddAnalyticsProcesses(this IServiceCollection services)
    {
        services
            .AddScoped<IAnalyticsProcess<BankAccountBalanceModel>, GenerateAccountBalanceSnapshot>()
            .AddScoped<IAnalyticsProcess<BankAccountBalanceModel>, GenerateAccountBalanceSummary>()
            .AddScoped<IAnalyticsProcess<BankAccountBalanceModel>, GenerateBusinessBalanceSnapshot>()
            .AddScoped<IAnalyticsProcess<MerchantTransactionModel>, CreateMerchantMetricSnapshot>();

        return services;
    }
}