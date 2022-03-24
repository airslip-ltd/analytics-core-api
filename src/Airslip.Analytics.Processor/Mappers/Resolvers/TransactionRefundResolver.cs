using Airslip.Analytics.Core.Models;
using Airslip.Common.Utilities;
using Airslip.MerchantIntegrations.Types.Models;
using AutoMapper;
using JetBrains.Annotations;

namespace Airslip.Analytics.Processor.Mappers.Resolvers;

[UsedImplicitly]
public class TransactionRefundResolver : IValueResolver<TransactionRefundItem, MerchantRefundItemModel, string>
{
    public string Resolve(TransactionRefundItem source, MerchantRefundItemModel destination, string destMember,
        ResolutionContext context)
    {
        return source.TransactionProductId ?? CommonFunctions.GetId();
    }
}