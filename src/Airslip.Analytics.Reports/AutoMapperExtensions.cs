using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Data;
using Airslip.Analytics.Reports.Models;
using AutoMapper;

namespace Airslip.Analytics.Reports
{
    public static class AutoMapperExtensions
    {
        public static IMapperConfigurationExpression AddReportingMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BankTransactionReportQuery, BankTransactionReportResponse>();
            cfg.CreateMap<CommerceTransactionReportQuery, CommerceTransactionReportResponse>();
            cfg.CreateMap<CommerceTransactionDownloadQuery, MerchantTransactionModel>();
            cfg.CreateMap<MerchantProduct, MerchantProductModel>();
            cfg.CreateMap<MerchantRefund, MerchantRefundModel>();
            cfg.CreateMap<MerchantRefundItem, MerchantRefundItemModel>();

            return cfg;
        }
    }
}