using Airslip.Common.Repository.Types.Interfaces;

namespace Airslip.Analytics.Core.Models;

public class MerchantRefundItemModel : IModelWithId
{
    public string Id { get; set; } = string.Empty;
    public string? TransactionProductId { get; init; }
    public string? ProductId { get; init; }
    public string? VariantId { get; init; }
    public double? Qty { get; set; }
    public long? Refund { get; set; }
}