using Airslip.Analytics.Core.Models;
using Airslip.MerchantIntegrations.Types.Models;
using AutoMapper;
using JetBrains.Annotations;
using System.Linq;

namespace Airslip.Analytics.Processor.Mappers.Resolvers;

[UsedImplicitly]
public class TransactionDescriptionResolver : IValueResolver<TransactionEnvelope, MerchantTransactionModel, string?>
{
    public string? Resolve(TransactionEnvelope source, MerchantTransactionModel destination, string? destMember,
        ResolutionContext context)
    {
        if (source.Transaction.Products == null || source.Transaction.Products.Count == 0) return source.Transaction.TransactionNumber;

        return source.Transaction.Products.First().Name;
    }
}