using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.MerchantIntegrations.Types.Enums;

namespace Airslip.Analytics.Core.Entities;

public class MerchantProduct : IEntityWithId
{
    public string Id { get; set; } = string.Empty;
    public string? TransactionProductId { get; init; }
    public string? ParentTransactionProductId { get; init; }
    public string? ProductId { get; init; }
    public string? Manufacturer { get; init; }
    public string? ModelNumber { get; init; }
    public string? ProductCode { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public long? Price { get; init; }
    public long? PriceIncTax { get; init; }
    public double? Quantity { get; init; }
    public double? QuantityRefunded { get; set; }
    public long? DiscountAmount { get; init; }
    public long? TotalPrice { get; init; }
    public long? TotalRefund { get; set; }
    public double? TaxPercent { get; init; }
    public long? TaxValue { get; init; }
    public long? TaxValueAfterDiscount { get; init; }
    public string? VariantId { get; init; }
    public string? WeightUnit { get; init; }
    public double? Weight { get; init; }
    public string? Sku { get; init; }
    public string? Ean { get; init; }
    public string? Upc { get; init; }
    public long? WarrantyExpiryDateTime { get; init; }
    public string? ImageUrl { get; init; }
    public string? Url { get; init; }
    public string? ManualUrl { get; init; }
    public string? Dimensions { get; init; }
    public ProductStatus ProductStatus { get; init; } = ProductStatus.Active;
}