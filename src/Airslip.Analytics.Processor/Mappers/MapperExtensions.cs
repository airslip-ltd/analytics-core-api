using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw;
using Airslip.Common.Utilities;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Airslip.Analytics.Processor.Mappers;

public static class MapperExtensions
{
    public static void AddRawYapilyData(this IMapperConfigurationExpression mapperConfigurationExpression)
    {
        mapperConfigurationExpression
            .CreateMap<RawBankModel, BankModel>()
            .ForPath(o => o.CountryCodes, 
                exp => exp.MapFrom(model => model
                    .CountryCodes.Select(p => new BankCountryCodeModel()
                    {
                        Id = p
                    })));
        mapperConfigurationExpression
            .CreateMap<RawAccountModel, AccountModel>();
        mapperConfigurationExpression
            .CreateMap<RawTransactionModel, TransactionModel>()
            .ForPath(o => o.Amount,
                exp => exp.MapFrom(model => (long) (model.Amount * 100) ));
        
        // Ignore the incoming Id so we can create a new entry every time we receive an update
        mapperConfigurationExpression.CreateMap<RawYapilyBalanceModel, AccountBalanceModel>()
            .ForPath(o => o.Id, _ => CommonFunctions.GetId());
        mapperConfigurationExpression.CreateMap<RawYapilyBalanceDetailModel, AccountBalanceDetailModel>();
        mapperConfigurationExpression.CreateMap<RawYapilyCreditLineModel, AccountBalanceCreditLineModel>();
    }
}