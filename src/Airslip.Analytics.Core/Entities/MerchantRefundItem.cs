using Airslip.Common.Repository.Types.Interfaces;

namespace Airslip.Analytics.Core.Entities;

public class MerchantRefundItem : IEntityWithId
{
    public string Id { get; set; }
    public string? TransactionProductId { get; init; }
    public string? ProductId { get; init; }
    public string? VariantId { get; init; }
    public double? Qty { get; set; }
    public long? Refund { get; set; }
}