using Airslip.Analytics.Reports.Data;
using Airslip.Analytics.Reports.Models;
using AutoMapper;

namespace Airslip.Analytics.Reports
{
    public static class AutoMapperExtensions
    {
        public static IMapperConfigurationExpression AddReportingMappings(this IMapperConfigurationExpression mapperConfigurationExpression)
        {
            mapperConfigurationExpression
                .CreateMap<BankTransactionReportQuery, BankTransactionReportResponse>();
            mapperConfigurationExpression
                .CreateMap<CommerceTransactionReportQuery, CommerceTransactionReportResponse>();

            return mapperConfigurationExpression;
        }
    }
}