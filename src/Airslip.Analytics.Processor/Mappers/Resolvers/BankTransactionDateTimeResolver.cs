using Airslip.Analytics.Core.Models;
using Airslip.Common.Utilities.Extensions;
using Airslip.Integrations.Banking.Types.Models;
using AutoMapper;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Processor.Mappers.Resolvers;

[UsedImplicitly]
public class BankTransactionDateTimeResolver : IValueResolver<BankingTransactionModel, BankTransactionModel, int?>
{
    public int? Resolve(BankingTransactionModel source, BankTransactionModel destination, int? destMember,
        ResolutionContext context)
    {
        DateTime theDate = source.CapturedDate.ToUtcDate();
        destination.Year = theDate.Year;
        destination.Month = theDate.Month;
        destination.Day = theDate.Day;
        return theDate.Year;
    }
}