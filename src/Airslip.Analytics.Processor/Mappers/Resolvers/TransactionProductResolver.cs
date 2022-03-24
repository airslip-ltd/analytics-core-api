using Airslip.Analytics.Core.Models;
using Airslip.Common.Utilities;
using Airslip.MerchantIntegrations.Types.Models;
using AutoMapper;
using JetBrains.Annotations;

namespace Airslip.Analytics.Processor.Mappers.Resolvers;

[UsedImplicitly]
public class TransactionProductResolver : IValueResolver<TransactionProduct, MerchantProductModel, string>
{
    public string Resolve(TransactionProduct source, MerchantProductModel destination, string destMember,
        ResolutionContext context)
    {
        return source.TransactionProductId ?? CommonFunctions.GetId();
    }
}