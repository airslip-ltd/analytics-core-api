using Airslip.Common.Repository.Types.Interfaces;

namespace Airslip.Analytics.Core.Entities;

public class MerchantDiscount : IEntityWithId
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; init; }
    public long? Amount { get; init; }
}
