using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw.Api2Cart;
using Airslip.Analytics.Processor.Mappers.Resolvers;
using Airslip.Common.CustomerPortal.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities.Extensions;
using Airslip.Integrations.Banking.Types.Enums;
using Airslip.Integrations.Banking.Types.Models;
using Airslip.MerchantIntegrations.Types.Models;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System;
using System.Linq;
using IModelWithId = Airslip.Common.Repository.Types.Interfaces.IModelWithId;

namespace Airslip.Analytics.Processor.Mappers;

public static class MapperExtensions
{
    public static IMapperConfigurationExpression AddBankingData(this IMapperConfigurationExpression mapperConfigurationExpression)
    {
        mapperConfigurationExpression
            .CreateMap<BankingBankModel, BankModel>()
            .ForPath(o => o.CountryCodes, 
                exp => exp.MapFrom(model => model
                    .CountryCodes.Select(p => new BankCountryCodeModel()
                    {
                        Id = p
                    })));
        mapperConfigurationExpression
            .CreateMap<BankingTransactionModel, BankTransactionModel>()
            .ForMember(o => o.Year,
                opt => opt.MapFrom<BankTransactionDateTimeResolver>())
            .ForMember(o => o.BankId,
                opt => opt.MapFrom(p=> p.BankingBankId))
            .ForMember(o => o.IntegrationId,
                opt => opt.MapFrom(p=> p.BankingAccountId));
        
        mapperConfigurationExpression
            .CreateMap<BankingAccountModel, IntegrationModel>()
            .ForPath(o => o.Name, 
                exp => exp.MapFrom(
                    p => $"{p.AccountNumber ?? p.LastCardDigits}"))
            .ForPath(o => o.IntegrationProviderId, 
                exp => exp.MapFrom(
                    p => p.BankingBankId))
            .ForPath(o => o.AuthenticationState, 
                exp => exp.MapFrom(
                    p => p.AccountStatus == BankingAccountStatus.Active 
                        ? AuthenticationState.Authenticated 
                        : AuthenticationState.NotAuthenticated))
            .ForPath(o => o.IntegrationType, 
                exp => exp.MapFrom(
                    p => IntegrationType.Banking))
            .ForMember(o => o.AccountDetail,
                exp => exp.MapFrom(p => new IntegrationAccountDetailModel()
                {
                    Id = p.Id!,
                    AccountId = p.AccountId,
                    AccountNumber = p.AccountNumber,
                    AccountStatus = p.AccountStatus,
                    AccountType = p.AccountType,
                    CurrencyCode = p.CurrencyCode,
                    SortCode = p.SortCode,
                    UsageType = p.UsageType,
                    LastCardDigits = p.LastCardDigits
                }));
        
        // Ignore the incoming Id so we can create a new entry every time we receive an update
        mapperConfigurationExpression.CreateMap<BankingBalanceModel, BankAccountBalanceModel>()
            .ForMember(o => o.Id, 
            opt => opt.MapFrom<UniqueIdResolver<BankingBalanceModel, BankAccountBalanceModel>>())
            .ForMember(o => o.IntegrationId,
                opt => opt.MapFrom(p=> p.BankingAccountId));

        mapperConfigurationExpression.CreateMap<BankingSyncRequestModel, BankSyncRequestModel>()
            .ForMember(o => o.IntegrationId, 
                exp => exp
                    .MapFrom(p => p.BankingAccountId));
        return mapperConfigurationExpression;
    }
    
    public static IMapperConfigurationExpression AddRawApi2CartData(this IMapperConfigurationExpression mapperConfigurationExpression)
    {
        mapperConfigurationExpression
            .CreateMap<RawApi2CartAccountModel, IntegrationModel>()
            .ForPath(o => o.AuthenticationState, 
                exp => exp.MapFrom(model => model
                    .State.CurrentState))
            .ForPath(o => o.IntegrationProviderId, 
                exp => exp.MapFrom(model => 
                    model.Provider.ToLower()))
            .ForPath(o => o.IntegrationType, 
                exp => exp.MapFrom(model => IntegrationType.Commerce))
            .ForPath(o => o.Name, 
                exp => 
                    exp.MapFrom(model => string.IsNullOrEmpty(model.Name) ? model.Provider : model.Name))
            .ForPath(o => o.AccountDetail, 
                exp => exp.Ignore());

        return mapperConfigurationExpression;
    }
    
    public static IMapperConfigurationExpression AddRawTransaction(this IMapperConfigurationExpression mapperConfigurationExpression)
    {
        mapperConfigurationExpression
            .CreateMap<TransactionEnvelope, MerchantTransactionModel>()
            .ForPath(o => o.Id,
                exp =>
                    exp.MapFrom(model => model.Transaction.TransactionId))
            .ForMember(o => o.TimeStamp, opt => 
                opt.MapFrom(p => DateTime.UtcNow.ToUnixTimeMilliseconds()))
            .ForMember(o => o.Datetime, exp =>
                exp.MapFrom<TransactionDateTimeResolver>())
            .ForPath(o => o.Date, exp =>
                exp.MapFrom(model =>
                    model.Transaction.TransactionDetail != null ? model.Transaction.TransactionDetail.Date : null))
            .ForPath(o => o.Number, exp =>
                exp.MapFrom(model =>
                    model.Transaction.TransactionDetail != null ? model.Transaction.TransactionDetail.Number : null))
            .ForPath(o => o.Source, exp =>
                exp.MapFrom(model => model.Transaction.Source))
            .ForMember(o => o.Description, exp =>
                exp.MapFrom<TransactionDescriptionResolver>())
            .ForPath(o => o.Store, exp =>
                exp.MapFrom(model =>
                    model.Transaction.TransactionDetail != null ? model.Transaction.TransactionDetail.Store : null))
            .ForPath(o => o.Subtotal, exp =>
                exp.MapFrom(model => model.Transaction.Subtotal))
            .ForPath(o => o.Till, exp =>
                exp.MapFrom(model =>
                    model.Transaction.TransactionDetail != null ? model.Transaction.TransactionDetail.Till : null))
            .ForPath(o => o.Time, exp =>
                exp.MapFrom(model =>
                    model.Transaction.TransactionDetail != null ? model.Transaction.TransactionDetail.Time : null))
            .ForPath(o => o.Total, exp =>
                exp.MapFrom(model => model.Transaction.Total))
            .ForPath(o => o.CurrencyCode, exp =>
                exp.MapFrom(model => model.Transaction.CurrencyCode))
            .ForPath(o => o.CustomerEmail, exp =>
                exp.MapFrom(model => model.Transaction.CustomerEmail))
            .ForPath(o => o.DataSource, exp =>
                exp.MapFrom(model => DataSources.Unknown))
            .ForPath(o => o.EntityId, exp =>
                exp.MapFrom(model => model.EntityId))
            .ForPath(o => o.InternalId, exp =>
                exp.MapFrom(model => model.Transaction.InternalId))
            .ForPath(o => o.OnlinePurchase, exp =>
                exp.MapFrom(model => model.Transaction.OnlinePurchase))
            .ForPath(o => o.OperatorName, exp =>
                exp.MapFrom(model => model.Transaction.OperatorName))
            .ForPath(o => o.RefundCode, exp =>
                exp.MapFrom(model => model.Transaction.RefundCode))
            .ForPath(o => o.ServiceCharge, exp =>
                exp.MapFrom(model => model.Transaction.ServiceCharge))
            .ForPath(o => o.StoreAddress, exp =>
                exp.MapFrom(model => model.Transaction.StoreAddress))
            .ForPath(o => o.StoreLocationId, exp =>
                exp.MapFrom(model => model.Transaction.StoreLocationId))
            .ForPath(o => o.TrackingId, exp =>
                exp.MapFrom(model => model.TrackingId))
            .ForPath(o => o.TransactionNumber, exp =>
                exp.MapFrom(model => model.Transaction.TransactionNumber))
            .ForPath(o => o.IntegrationId, exp =>
                exp.MapFrom(model => model.AccountId))
            .ForPath(o => o.UserId, exp =>
                exp.MapFrom(model => model.UserId))
            .ForPath(o => o.AirslipUserType, exp =>
                exp.MapFrom(model => model.AirslipUserType))
            .ForPath(o => o.BankStatementDescription, exp =>
                exp.MapFrom(model => model.Transaction.BankStatementDescription))
            .ForPath(o => o.BankStatementTransactionIdentifier, exp =>
                exp.MapFrom(model => model.Transaction.BankStatementTransactionIdentifier))
            
            .ForPath(o => o.Products, exp =>
                exp.MapFrom(model => model.Transaction.Products))
            .ForPath(o => o.Refunds, exp =>
                exp.MapFrom(model => model.Transaction.Refunds))
            .ForMember(o => o.OrderStatus, exp =>
                exp.MapFrom<TransactionStatusResolver>())
            .ForMember(o => o.PaymentStatus, exp =>
                exp.MapFrom<TransactionPaymentStatusResolver>())
            .ForMember(o => o.TotalRefund, exp =>
                exp.MapFrom(model => model.Transaction.TotalSummary!.Refunded));

        mapperConfigurationExpression
            .CreateMap<TransactionRefundDetail, MerchantRefundModel>()
            .ForMember(o => o.ModifiedTime, exp =>
                exp.MapFrom<DateTimeResolver>())
            .ForPath(o => o.Items, exp =>
                exp.MapFrom(s => s.Items));
        
        mapperConfigurationExpression
            .CreateMap<TransactionRefundItem, MerchantRefundItemModel>()
            .ForMember(o => o.Id, exp =>
                exp.MapFrom<TransactionRefundResolver>());
        
        mapperConfigurationExpression
            .CreateMap<TransactionProduct, MerchantProductModel>()
            .ForMember(o => o.Id, exp =>
                exp.MapFrom<TransactionProductResolver>())
            .ForMember(o => o.QuantityRefunded, exp =>
                exp.MapFrom(s => s.Refund!.Qty))
            .ForMember(o => o.TotalRefund, exp =>
                exp.MapFrom(s => s.Refund!.Refund));
        
        return mapperConfigurationExpression;
    }
    
    public static IMapperConfigurationExpression AddEntityModelMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.AddCollectionMappers();
        cfg.CreateMap<BankAccountBalanceModel, BankAccountBalance>().ReverseMap();
        cfg.CreateMap<IntegrationModel, Integration>().ReverseMap();
        cfg.CreateMap<BankModel, Bank>().ReverseMap();
        cfg.CreateMap<BankTransactionModel, BankTransaction>().ReverseMap();
        cfg.CreateMap<BankCountryCodeModel, BankCountryCode>().ReverseMap();
        cfg.CreateMap<BankSyncRequestModel, BankSyncRequest>().ReverseMap();
        cfg.CreateMap<IntegrationAccountDetail, IntegrationAccountDetailModel>()
            .ForPath(d => d.Id, c => c
                .MapFrom(s => s.Id))
            .ReverseMap();
        cfg.CreateMap<MerchantTransactionModel, MerchantTransaction>().ReverseMap();
        cfg.CreateMap<MerchantProductModel, MerchantProduct>().MatchOnId().ReverseMap();
        cfg.CreateMap<MerchantRefundModel, MerchantRefund>().MatchOnId().ReverseMap();
        cfg.CreateMap<MerchantRefundItemModel, MerchantRefundItem>()
            .ForMember(o => o.Id, exp => exp.MapFrom(s => s.TransactionProductId))
            .MatchOnId()
            .ReverseMap();
        
        cfg.CreateMap<RelationshipHeaderModel, RelationshipHeader>().ReverseMap();
        cfg.CreateMap<RelationshipDetailModel, RelationshipDetail>().ReverseMap();
        
        return cfg;
    }

    private static IMappingExpression<TSource, TDestination> MatchOnId<TSource, TDestination>(this 
        IMappingExpression<TSource, TDestination> expression)
        where TSource : IModelWithId
        where TDestination : IEntityWithId
    {
        return expression
                .EqualityComparison((dto, entity) => dto.Id == entity.Id);
    }
}