using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw;
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
            .CreateMap<RawYapilyAccountModel, AccountModel>();
        mapperConfigurationExpression
            .CreateMap<RawYapilyTransactionModel, TransactionModel>()
            .ForPath(o => o.Amount,
                exp => exp.MapFrom(model => (long) (model.Amount * 100) ));
        
        // Ignore the incoming Id so we can create a new entry every time we receive an update
        mapperConfigurationExpression.CreateMap<RawYapilyBalanceModel, AccountBalanceModel>()
            .ForMember(o => o.Id, 
            opt => opt.MapFrom<CustomResolver>())
            .ForMember(o => o.AccountId,
                opt => opt.MapFrom(p=> p.Id));
        mapperConfigurationExpression.CreateMap<RawYapilyBalanceDetailModel, AccountBalanceDetailModel>();
        mapperConfigurationExpression.CreateMap<RawYapilyCreditLineModel, AccountBalanceCreditLineModel>();

        mapperConfigurationExpression.CreateMap<RawYapilySyncRequestModel, SyncRequestModel>()
            .ForMember(o => o.AccountId, 
                exp => exp
                    .MapFrom(p => p.YapilyAccountId));
        return mapperConfigurationExpression;
    }

    [UsedImplicitly]
    private class CustomResolver : IValueResolver<RawYapilyBalanceModel, AccountBalanceModel, string?>
    {
        public string Resolve(RawYapilyBalanceModel source, AccountBalanceModel destination, string? member, 
            ResolutionContext context)
        {
            return CommonFunctions.GetId();
        }
    }

    public static IMapperConfigurationExpression AddEntityModelMappings(this IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<AccountBalanceModel, AccountBalance>().ReverseMap();
        cfg.CreateMap<AccountBalanceDetailModel, AccountBalanceDetail>().ReverseMap();
        cfg.CreateMap<AccountBalanceCreditLineModel, AccountBalanceCreditLine>().ReverseMap();
        cfg.CreateMap<AccountModel, Account>().ReverseMap();
        cfg.CreateMap<BankModel, Bank>().ReverseMap();
        cfg.CreateMap<TransactionModel, Transaction>().ReverseMap();
        cfg.CreateMap<BankCountryCodeModel, BankCountryCode>().ReverseMap();
        cfg.CreateMap<SyncRequestModel, SyncRequest>().ReverseMap();

        return cfg;
    }
}