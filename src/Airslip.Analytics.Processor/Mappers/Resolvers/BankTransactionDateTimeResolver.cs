using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw.Yapily;
using Airslip.Common.Utilities.Extensions;
using AutoMapper;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Processor.Mappers.Resolvers;

[UsedImplicitly]
public class BankTransactionDateTimeResolver : IValueResolver<RawYapilyTransactionModel, BankTransactionModel, int?>
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