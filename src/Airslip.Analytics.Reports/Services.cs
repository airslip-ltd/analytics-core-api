using Airslip.Analytics.Reports.Implementations;
using Airslip.Analytics.Reports.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Airslip.Analytics.Reports;

public static class Services
{
    public static IServiceCollection AddReportingServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IBankTransactionReport, BankTransactionReport>();
    }
}