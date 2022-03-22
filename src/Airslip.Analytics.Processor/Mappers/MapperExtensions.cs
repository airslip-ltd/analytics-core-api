using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw.Api2Cart;
using Airslip.Analytics.Core.Models.Raw.Yapily;
using Airslip.Common.CustomerPortal.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Transaction;
using Airslip.Common.Utilities;
using Airslip.Common.Utilities.Extensions;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using JetBrains.Annotations;
using System;
using System.Linq;
using IModelWithId = Airslip.Common.Repository.Types.Interfaces.IModelWithId;

namespace Airslip.Analytics.Processor.Mappers;

public static class MapperExtensions
{
    public static IMapperConfigurationExpression AddRawYapilyData(this IMapperConfigurationExpression mapperConfigurationExpression)
    {
        mapperConfigurationExpression
            .CreateMap<RawYapilyBankModel, BankModel>()
            .ForPath(o => o.EnvironmentType, 
                exp => exp.MapFrom(model => 
                    model.EnvironmentType == "LIVE" ? EnvironmentType.Live : EnvironmentType.Sandbox))
            .ForPath(o => o.CountryCodes, 
                exp => exp.MapFrom(model => model
                    .CountryCodes.Select(p => new BankCountryCodeModel()
                    {
                        Id = p
                    })));
        mapperConfigurationExpression
            .CreateMap<RawYapilyTransactionModel, BankTransactionModel>()
            .ForPath(o => o.Amount,
                exp => exp.MapFrom(model => (long) (model.Amount * 100) ))
            .ForMember(o => o.Year,
                opt => opt.MapFrom<BankTransactionDateTimeResolver>())
            .ForMember(o => o.IntegrationId,
                opt => opt.MapFrom(p=> p.YapilyAccountId));
        
        mapperConfigurationExpression
            .CreateMap<RawYapilyAccountModel, IntegrationModel>()
            .ForPath(o => o.Name, 
                exp => exp.MapFrom(
                    p => $"{p.AccountNumber ?? p.LastCardDigits}"))
            .ForPath(o => o.IntegrationProviderId, 
                exp => exp.MapFrom(
                    p => p.InstitutionId))
            .ForPath(o => o.AuthenticationState, 
                exp => exp.MapFrom(
                    p => p.AccountStatus == AccountStatus.Active ? AuthenticationState.Authenticated : AuthenticationState.NotAuthenticated))
            .ForPath(o => o.DataSource, 
                exp => exp.MapFrom(
                    p => DataSources.Yapily))
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
        mapperConfigurationExpression.CreateMap<RawYapilyBalanceModel, BankAccountBalanceModel>()
            .ForMember(o => o.Id, 
            opt => opt.MapFrom<UniqueIdResolver<RawYapilyBalanceModel, BankAccountBalanceModel>>())
            .ForMember(o => o.IntegrationId,
                opt => opt.MapFrom(p=> p.YapilyAccountId));
        mapperConfigurationExpression.CreateMap<RawYapilyBalanceDetailModel, BankAccountBalanceDetailModel>();
        mapperConfigurationExpression.CreateMap<RawYapilyCreditLineModel, BankAccountBalanceCreditLineModel>();

        mapperConfigurationExpression.CreateMap<RawYapilySyncRequestModel, BankSyncRequestModel>()
            .ForMember(o => o.IntegrationId, 
                exp => exp
                    .MapFrom(p => p.YapilyAccountId));
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
                exp.MapFrom(model => model.Transaction.Refunds));

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
                exp.MapFrom<TransactionProductResolver>());
        
        return mapperConfigurationExpression;
    }

    [UsedImplicitly]
    private class UniqueIdResolver<TSource, TDest> : IValueResolver<TSource, TDest, string?>
    {
        public string Resolve(TSource source, TDest destination, string? member, 
            ResolutionContext context)
        {
            return CommonFunctions.GetId();
        }
    }
    
    [UsedImplicitly]
    private class TransactionProductResolver : IValueResolver<TransactionProduct, MerchantProductModel, string>
    {
        public string Resolve(TransactionProduct source, MerchantProductModel destination, string destMember,
            ResolutionContext context)
        {
            return source.TransactionProductId ?? CommonFunctions.GetId();
        }
    }
    
    [UsedImplicitly]
    private class TransactionRefundResolver : IValueResolver<TransactionRefundItem, MerchantRefundItemModel, string>
    {
        public string Resolve(TransactionRefundItem source, MerchantRefundItemModel destination, string destMember,
            ResolutionContext context)
        {
            return source.TransactionProductId ?? CommonFunctions.GetId();
        }
    }
    
    [UsedImplicitly]
    private class DateTimeResolver : IValueResolver<TransactionRefundDetail, MerchantRefundModel, DateTime>
    {
        public DateTime Resolve(TransactionRefundDetail source, MerchantRefundModel destination, DateTime destMember,
            ResolutionContext context)
        {
            if (source.ModifiedTime?.Value != null && source.ModifiedTime.Format != null)
                return DateTime.ParseExact(source.ModifiedTime.Value, source.ModifiedTime.Format, null);
            return DateTime.UtcNow;
        }
    }
    
    [UsedImplicitly]
    private class TransactionDateTimeResolver : IValueResolver<TransactionEnvelope, MerchantTransactionModel, DateTime?>
    {
        public DateTime? Resolve(TransactionEnvelope source, MerchantTransactionModel destination, DateTime? destMember,
            ResolutionContext context)
        {
            if (source.Transaction.Datetime == null) return null;
            
            DateTime theDate = DateTime.Parse(source.Transaction.Datetime, null,
                System.Globalization.DateTimeStyles.RoundtripKind);
            destination.Year = theDate.Year;
            destination.Month = theDate.Month;
            destination.Day = theDate.Day;
            return theDate;
        }
    }
    
    public class TransactionDescriptionResolver : IValueResolver<TransactionEnvelope, MerchantTransactionModel, string?>
    {
        public string? Resolve(TransactionEnvelope source, MerchantTransactionModel destination, string? destMember,
            ResolutionContext context)
        {
            if (source.Transaction.Products == null || source.Transaction.Products.Count == 0) return source.Transaction.TransactionNumber;

            return source.Transaction.Products.First().Name;
        }
    }
    
    [UsedImplicitly]
    private class BankTransactionDateTimeResolver : IValueResolver<RawYapilyTransactionModel, BankTransactionModel, int?>
    {
        public int? Resolve(RawYapilyTransactionModel source, BankTransactionModel destination, int? destMember,
            ResolutionContext context)
        {
            DateTime theDate = source.CapturedDate.ToUtcDate();
            destination.Year = theDate.Year;
            destination.Month = theDate.Month;
            destination.Day = theDate.Day;
            return theDate.Year;
        }
    }
    
    public static IMapperConfigurationExpression AddEntityModelMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.AddCollectionMappers();
        cfg.CreateMap<BankAccountBalanceModel, BankAccountBalance>().ReverseMap();
        cfg.CreateMap<BankAccountBalanceDetailModel, BankAccountBalanceDetail>().ReverseMap();
        cfg.CreateMap<BankAccountBalanceCreditLineModel, BankAccountBalanceCreditLine>().ReverseMap();
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