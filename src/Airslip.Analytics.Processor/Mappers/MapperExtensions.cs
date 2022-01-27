using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw.Yapily;
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

namespace Airslip.Analytics.Processor.Mappers;

public static class MapperExtensions
{
    public static IMapperConfigurationExpression AddRawYapilyData(this IMapperConfigurationExpression mapperConfigurationExpression)
    {
        mapperConfigurationExpression
            .CreateMap<RawYapilyBankModel, BankModel>()
            .ForPath(o => o.CountryCodes, 
                exp => exp.MapFrom(model => model
                    .CountryCodes.Select(p => new BankCountryCodeModel()
                    {
                        Id = p
                    })));
        mapperConfigurationExpression
            .CreateMap<RawYapilyAccountModel, BankAccountModel>();
        mapperConfigurationExpression
            .CreateMap<RawYapilyTransactionModel, BankTransactionModel>()
            .ForPath(o => o.Amount,
                exp => exp.MapFrom(model => (long) (model.Amount * 100) ));
        
        // Ignore the incoming Id so we can create a new entry every time we receive an update
        mapperConfigurationExpression.CreateMap<RawYapilyBalanceModel, BankAccountBalanceModel>()
            .ForMember(o => o.Id, 
            opt => opt.MapFrom<CustomResolver>())
            .ForMember(o => o.AccountId,
                opt => opt.MapFrom(p=> p.Id));
        mapperConfigurationExpression.CreateMap<RawYapilyBalanceDetailModel, BankAccountBalanceDetailModel>();
        mapperConfigurationExpression.CreateMap<RawYapilyCreditLineModel, BankAccountBalanceCreditLineModel>();

        mapperConfigurationExpression.CreateMap<RawYapilySyncRequestModel, BankSyncRequestModel>()
            .ForMember(o => o.AccountId, 
                exp => exp
                    .MapFrom(p => p.YapilyAccountId));
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
            // .ForPath(o => o.Datetime, exp =>
            //     exp.MapFrom(model =>
            //         model.Transaction.Datetime == null
            //             ? (DateTime?) null
            //             : DateTime.Parse(model.Transaction.Datetime, null,
            //                 System.Globalization.DateTimeStyles.RoundtripKind)))
            
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
                exp.MapFrom(s => s.TransactionProductId));
        
        mapperConfigurationExpression
            .CreateMap<TransactionProduct, MerchantProductModel>()
            .ForMember(o => o.Id, exp =>
                exp.MapFrom(s => s.TransactionProductId));
        
        return mapperConfigurationExpression;
    }

    [UsedImplicitly]
    private class CustomResolver : IValueResolver<RawYapilyBalanceModel, BankAccountBalanceModel, string?>
    {
        public string Resolve(RawYapilyBalanceModel source, BankAccountBalanceModel destination, string? member, 
            ResolutionContext context)
        {
            return CommonFunctions.GetId();
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
    
    public static IMapperConfigurationExpression AddEntityModelMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.AddCollectionMappers();
        cfg.CreateMap<BankAccountBalanceModel, BankAccountBalance>().ReverseMap();
        cfg.CreateMap<BankAccountBalanceDetailModel, BankAccountBalanceDetail>().ReverseMap();
        cfg.CreateMap<BankAccountBalanceCreditLineModel, BankAccountBalanceCreditLine>().ReverseMap();
        cfg.CreateMap<BankAccountModel, BankAccount>().ReverseMap();
        cfg.CreateMap<BankModel, Bank>().ReverseMap();
        cfg.CreateMap<BankTransactionModel, BankTransaction>().ReverseMap();
        cfg.CreateMap<BankCountryCodeModel, BankCountryCode>().ReverseMap();
        cfg.CreateMap<BankSyncRequestModel, BankSyncRequest>().ReverseMap();

        cfg.CreateMap<MerchantTransactionModel, MerchantTransaction>().ReverseMap();
        cfg.CreateMap<MerchantProductModel, MerchantProduct>().MatchOnId().ReverseMap();
        cfg.CreateMap<MerchantRefundModel, MerchantRefund>().MatchOnId().ReverseMap();
        cfg.CreateMap<MerchantRefundItemModel, MerchantRefundItem>()
            .ForMember(o => o.Id, exp => exp.MapFrom(s => s.TransactionProductId))
            .MatchOnId().ReverseMap();
        
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