namespace Airslip.Analytics.Core.Models.Raw.CustomerPortal;

public class RawPartnerDataPermissionModel
{
    public long? ApprovedOn { get; init; }
    public string? ApprovedByUserId { get; init; }
    public string PermissionType { get; init; } = string.Empty;
    public long RequestedOn { get; init; }
    public string RequestedByUserId { get; init; } = string.Empty;
}