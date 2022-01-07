using Airslip.Analytics.Core.Models;
using AutoMapper;
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
    }
}