using Airslip.Analytics.Core.Models;
using Airslip.MerchantIntegrations.Types.Models;
using AutoMapper;
using JetBrains.Annotations;

namespace Airslip.Analytics.Processor.Mappers.Resolvers;

[UsedImplicitly]
public class TransactionPaymentStatusResolver : IValueResolver<TransactionEnvelope, MerchantTransactionModel, string>
{
    public string Resolve(TransactionEnvelope source, MerchantTransactionModel destination, string? destMember,
        ResolutionContext context)
    {
        return source.Transaction.PaymentStatus?.Name ?? "Unknown";
    }
}