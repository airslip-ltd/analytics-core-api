using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Core.Entities;

public class RelationshipDetail : IEntityWithId
{
    public string Id { get; set; }
    public string RelationshipHeaderId { get; set; } = string.Empty;
    
    public string ViewerEntityId { get; set; } = string.Empty;
    public AirslipUserType ViewerAirslipUserType { get; set; }
    
    public string OwnerEntityId { get; set; } = string.Empty;
    public AirslipUserType OwnerAirslipUserType { get; set; }
    
    public string PermissionType { get; set; }
    public bool Allowed { get; set; } = false;
    
    public virtual RelationshipHeader RelationshipHeader { get; set; } = null!;
}