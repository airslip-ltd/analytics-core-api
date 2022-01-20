using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw;
using Airslip.Analytics.Core.Models.Raw.Yapily;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Transaction;
using Airslip.Common.Utilities;
using Airslip.Common.Utilities.Extensions;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
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
                    exp.MapFrom(model => $"{model.EntityId}:{model.Transaction.TransactionNumber}"))
            .ForPath(o => o.Datetime, exp =>
                exp.MapFrom(model =>
                    model.Transaction.Datetime == null
                        ? (DateTime?) null
                        : DateTime.Parse(model.Transaction.Datetime, null,
                            System.Globalization.DateTimeStyles.RoundtripKind)))
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
            .ForPath(o => o.TimeStamp, exp =>
                exp.MapFrom(model => model.CreatedTimeStamp))
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
            .ForPath(o => o.Discounts, exp =>
                exp.MapFrom(model => (model.Transaction.Discounts ?? Array.Empty<DiscountRequest>()).Select(o => 
                    new MerchantDiscountModel
                    {
                        Id = CommonFunctions.GetId(),
                        Amount = o.Amount,
                        Name = o.Name
                    })))
            .ForPath(o => o.Products, exp =>
                exp.MapFrom(model => (model.Transaction.Products ?? Array.Empty<ProductRequest>()).Select(o => 
                    new MerchantProductModel
                    {
                        Id = CommonFunctions.GetId(),
                        Code = o.Code,
                        Description = o.Description,
                        Dimensions = o.Dimensions,
                        Ean = o.Ean,
                        Item = o.Item,
                        Manufacturer = o.Manufacturer,
                        Quantity = o.Quantity,
                        Sku = o.Sku,
                        Subtotal = o.Subtotal,
                        Total = o.Total,
                        Upc = o.Upc,
                        Url = o.Url,
                        ImageUrl = o.ImageUrl,
                        ManualUrl = o.ManualUrl,
                        ModelNumber = o.ModelNumber,
                        ReleaseDate = o.ReleaseDate,
                        VatAmount = o.VatRate != null ? o.VatRate.Amount : null,
                        VatRate =  o.VatRate != null ? o.VatRate.Rate : null,
                        VatCode = o.VatRate != null ? o.VatRate.Code : null
                    })))
            .ForPath(o => o.PaymentDetails, exp =>
                exp.MapFrom(model => (model.Transaction.PaymentDetails ?? Array.Empty<PaymentDetailRequest>()).Select(o => 
                    new MerchantPaymentDetailModel()
                    {
                        Id = CommonFunctions.GetId(),
                        Amount = o.Amount,
                        Method = o.Method,
                        CardDetails = (o.CardDetails ?? Array.Empty<CardDetailRequest>()).Select(p => new MerchantCardDetailModel()
                        {
                            Id = CommonFunctions.GetId(),
                            Aid = p.Aid,
                            Tid = p.Tid,
                            AuthCode = p.AuthCode,
                            CardScheme = p.CardScheme,
                            PanSequence = p.PanSequence,
                            MaskedPanNumber = p.MaskedPanNumber
                        }).ToList()
                    })))
            .ForPath(o => o.VatRates, exp =>
                exp.MapFrom(model => (model.Transaction.VatRates ?? Array.Empty<VatRequest>()).Select(o => 
                    new MerchantVatModel()
                    {
                        Id = CommonFunctions.GetId(),
                        Amount = o.Amount,
                        Code = o.Code,
                        Rate = o.Rate
                    })));

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

    public static IMapperConfigurationExpression AddEntityModelMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<BankAccountBalanceModel, BankAccountBalance>().ReverseMap();
        cfg.CreateMap<BankAccountBalanceDetailModel, BankAccountBalanceDetail>().ReverseMap();
        cfg.CreateMap<BankAccountBalanceCreditLineModel, BankAccountBalanceCreditLine>().ReverseMap();
        cfg.CreateMap<BankAccountModel, BankAccount>().ReverseMap();
        cfg.CreateMap<BankModel, Bank>().ReverseMap();
        cfg.CreateMap<BankTransactionModel, BankTransaction>().ReverseMap();
        cfg.CreateMap<BankCountryCodeModel, BankCountryCode>().ReverseMap();
        cfg.CreateMap<BankSyncRequestModel, BankSyncRequest>().ReverseMap();

        cfg.CreateMap<MerchantTransactionModel, MerchantTransaction>()
            .ForMember(o => o.TimeStamp, opt => 
                opt.MapFrom(p => DateTime.UtcNow.ToUnixTimeMilliseconds()))
            .ReverseMap();
        cfg.CreateMap<MerchantCardDetailModel, MerchantCardDetail>().MatchOnId().ReverseMap();
        cfg.CreateMap<MerchantPaymentDetailModel, MerchantPaymentDetail>().MatchOnId().ReverseMap();
        cfg.CreateMap<MerchantDiscountModel, MerchantDiscount>().MatchOnId().ReverseMap();
        cfg.CreateMap<MerchantProductModel, MerchantProduct>().MatchOnId().ReverseMap();
        cfg.CreateMap<MerchantVatModel, MerchantVat>().MatchOnId().ReverseMap();
        
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