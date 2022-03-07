using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Core.Models.Raw.CustomerPortal;

public class RawRelatedEntityModel
{
    public string? UserId { get; init; }
    public string? EntityId { get; init; }
    public AirslipUserType AirslipUserType { get; init; }
}