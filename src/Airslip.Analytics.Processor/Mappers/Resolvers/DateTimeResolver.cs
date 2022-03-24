using Airslip.Analytics.Core.Models;
using Airslip.MerchantIntegrations.Types.Models;
using AutoMapper;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Processor.Mappers.Resolvers;

[UsedImplicitly]
public class DateTimeResolver : IValueResolver<TransactionRefundDetail, MerchantRefundModel, DateTime>
{
    public DateTime Resolve(TransactionRefundDetail source, MerchantRefundModel destination, DateTime destMember,
        ResolutionContext context)
    {
        if (source.ModifiedTime?.Value != null && source.ModifiedTime.Format != null)
            return DateTime.ParseExact(source.ModifiedTime.Value, source.ModifiedTime.Format, null);
        return DateTime.UtcNow;
    }
}