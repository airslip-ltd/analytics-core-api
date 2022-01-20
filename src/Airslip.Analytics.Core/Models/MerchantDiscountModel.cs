namespace Airslip.Analytics.Core.Models;

public class MerchantDiscountModel : IModelWithId
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; init; }
    public long? Amount { get; init; }
}