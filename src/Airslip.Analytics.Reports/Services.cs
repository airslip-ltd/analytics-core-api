using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Reports.Implementations;
using Airslip.Analytics.Reports.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Airslip.Analytics.Reports;

public static class Services
{
    public static IServiceCollection AddReportingServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IDataListService, DataListService>()
            .AddScoped<ICommerceProviderReport, CommerceProviderReport>()
            .AddScoped<IAccountBalanceReport, AccountBalanceReport>()
            .AddScoped<IDownloadService, DownloadService>()
            .AddScoped<IBankTransactionReport, BankTransactionReport>()
            .AddScoped<ICommerceTransactionReport, CommerceTransactionReport>();
    }
}