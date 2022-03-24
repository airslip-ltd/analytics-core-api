using Airslip.Analytics.Core.Models;
using Airslip.MerchantIntegrations.Types.Models;
using AutoMapper;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Processor.Mappers.Resolvers;

[UsedImplicitly]
public class TransactionDateTimeResolver : IValueResolver<TransactionEnvelope, MerchantTransactionModel, DateTime?>
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