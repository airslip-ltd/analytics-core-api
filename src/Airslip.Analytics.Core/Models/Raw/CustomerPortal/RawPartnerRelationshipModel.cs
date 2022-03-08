using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Types.Enums;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models.Raw.CustomerPortal;

public record RawPartnerRelationshipModel
{
    public string? Id { get; init; }
    public string? UserId { get; init; }
    public string? EntityId { get; init; }
    public AirslipUserType AirslipUserType { get; init; }
    public EntityStatus EntityStatus { get; init; }
    public RelationshipStatus RelationshipStatus { get; init; }
    public RawRelatedEntityModel Related { get; init; } = new();
    public List<RawPartnerDataPermissionModel> Permission { get; init; } = new();
    public RawPartnerInvitationDetailModel InvitationDetails { get; init; } = new();
    public long TimeStamp { get; set; }
}