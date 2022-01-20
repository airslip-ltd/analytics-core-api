namespace Airslip.Analytics.Core.Models;

public class MerchantVatModel : IModelWithId
{
    public string Id { get; set; } = string.Empty;
    public string? Code { get; init; }
    public decimal? Rate { get; init; }
    public long? Amount { get; init; }
}