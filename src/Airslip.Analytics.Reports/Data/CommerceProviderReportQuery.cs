using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Reports.Interfaces;
using Airslip.Common.CustomerPortal.Enums;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Reports.Data;

public class CommerceProviderReportQuery : IEntity, IOwnedDataQuery
{
    public string Id { get; set; } = string.Empty;
    public virtual BasicAuditInformation? AuditInformation { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string IntegrationProviderId { get; set; } = string.Empty;
    public AuthenticationState AuthenticationState { get; init; } = AuthenticationState.Authenticating;
    public string Name { get; init; } = string.Empty;
    public long TimeStamp { get; set; }

    public string OwnerEntityId { get; init; }
    public AirslipUserType OwnerAirslipUserType { get; init; }
    public string ViewerEntityId { get; init; } = string.Empty;
    public AirslipUserType ViewerAirslipUserType { get; init; }
    public string PermissionType { get; init; } = string.Empty;
    public bool Allowed { get; init; }
}
