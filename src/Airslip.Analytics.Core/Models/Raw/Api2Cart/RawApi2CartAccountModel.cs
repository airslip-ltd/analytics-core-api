using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Core.Models.Raw.Api2Cart;

public record RawApi2CartAccountModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string? UserId { get; set; }
    public string? EntityId { get; set; } = string.Empty;
    public AirslipUserType AirslipUserType { get; set; }
    public string Provider { get; init; } = string.Empty;
    public RawApi2CartAccountStateModel State { get; init; } = new();
    public string Key { get; set; } = string.Empty;
    public string AirslipApiKey { get; private set; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public bool SupportsWebhooks { get; set; }
}