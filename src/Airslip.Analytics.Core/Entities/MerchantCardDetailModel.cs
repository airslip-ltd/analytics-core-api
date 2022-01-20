using Airslip.Common.Repository.Types.Interfaces;

namespace Airslip.Analytics.Core.Entities;

public class MerchantCardDetail : IEntityWithId
{
    public string Id { get; set; } = string.Empty;
    public string? AuthCode { get; init; }
    public string? Aid { get; init; }
    public string? Tid { get; init; }
    public string? MaskedPanNumber { get; init; }
    public string? PanSequence { get; init; }
    public string? CardScheme { get; init; }
}