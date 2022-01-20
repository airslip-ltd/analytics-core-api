namespace Airslip.Analytics.Core.Models;

public class MerchantProductModel : IModelWithId
{
    public string Id { get; set; } = string.Empty;
    public string? Item { get; init; }
    public string? Code { get; init; }
    public long? Subtotal { get; init; }
    public long? Total { get; init; }
    public int? Quantity { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
    public string? Url { get; init; }
    public string? ManualUrl { get; init; }
    public string? Dimensions { get; init; }
    public long? ReleaseDate { get; init; }
    public string? Manufacturer { get; init; }
    public string? ModelNumber { get; init; }
    public string? Sku { get; init; }
    public string? Ean { get; init; }
    public string? Upc { get; init; }
    public string? VatCode { get; init; }
    public decimal? VatRate { get; init; }
    public long? VatAmount { get; init; }
}