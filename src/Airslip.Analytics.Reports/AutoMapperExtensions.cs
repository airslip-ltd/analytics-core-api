using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Data;
using Airslip.Analytics.Reports.Models;
using AutoMapper;

namespace Airslip.Analytics.Reports;

public static class AutoMapperExtensions
{
    public static IMapperConfigurationExpression AddReportingMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<CommerceProviderReportQuery, CommerceProviderModel>();
        cfg.CreateMap<AccountBalanceReportQuery, AccountBalanceReportModel>();
        cfg.CreateMap<BankTransactionReportQuery, BankTransactionReportModel>();
        cfg.CreateMap<IntegrationProvider, IntegrationProviderReportModel>();
        cfg.CreateMap<Integration, IntegrationReportModel>();
        cfg.CreateMap<IntegrationAccountDetail, IntegrationAccountDetailReportModel>();
        cfg.CreateMap<CommerceTransactionReportQuery, CommerceTransactionReportModel>();
        cfg.CreateMap<CommerceTransactionDownloadQuery, MerchantTransactionModel>();
        cfg.CreateMap<MerchantProduct, MerchantProductModel>();
        cfg.CreateMap<MerchantRefund, MerchantRefundModel>();
        cfg.CreateMap<MerchantRefundItem, MerchantRefundItemModel>();

        return cfg;
    }
}