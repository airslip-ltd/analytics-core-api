using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw;
using Airslip.Analytics.Core.Models.Raw.Yapily;
using Airslip.Common.Utilities;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
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

        return cfg;
    }
}