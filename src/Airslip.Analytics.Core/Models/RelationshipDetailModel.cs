using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Core.Models;

public class RelationshipDetailModel : IModelWithId
{
    public string? Id { get; set; }
    public string ViewerEntityId { get; set; } = string.Empty;
    public AirslipUserType ViewerAirslipUserType { get; set; }
    public string OwnerEntityId { get; set; } = string.Empty;
    public AirslipUserType OwnerAirslipUserType { get; set; }
    public string PermissionType { get; set; } = string.Empty;
    public bool Allowed { get; set; } = false;
}