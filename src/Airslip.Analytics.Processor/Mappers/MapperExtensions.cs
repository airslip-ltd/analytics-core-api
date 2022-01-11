using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw;
using Airslip.Common.Utilities;
using AutoMapper;
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
            .ForPath(o => o.Id, _ => CommonFunctions.GetId());
        mapperConfigurationExpression.CreateMap<RawYapilyBalanceDetailModel, AccountBalanceDetailModel>();
        mapperConfigurationExpression.CreateMap<RawYapilyCreditLineModel, AccountBalanceCreditLineModel>();

        // mapperConfigurationExpression.CreateMap<RawYapilySyncRequestModel, SyncRequestModel>()
        //     .ForSourceMember(o => o.YapilyAccountId, 
        //         exp => exp.DoNotValidate());
        return mapperConfigurationExpression;
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
        
        // mapperConfigurationExpression.CreateMap<SyncRequestModel, SyncRequest>().ReverseMap();

        return cfg;
    }
}