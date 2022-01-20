using Airslip.Common.Repository.Types.Interfaces;

namespace Airslip.Analytics.Core.Entities;

public class MerchantVat : IEntityWithId
{
    public string Id { get; set; } = string.Empty;
    public string? Code { get; init; }
    public decimal? Rate { get; init; }
    public long? Amount { get; init; }
}
