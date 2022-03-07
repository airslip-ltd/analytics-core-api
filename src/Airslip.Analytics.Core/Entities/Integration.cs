using Airslip.Analytics.Core.Enums;
using Airslip.Common.CustomerPortal.Enums;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Core.Entities;

public record Integration : IEntityWithOwnership, IFromDataSource
{
    public string Id { get; set; } = string.Empty;
    public virtual BasicAuditInformation? AuditInformation { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string IntegrationProviderId { get; init; } = string.Empty;
    public IntegrationType IntegrationType { get; set; } = IntegrationType.Commerce; 
    public string Name { get; init; } = string.Empty;
    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public AuthenticationState AuthenticationState { get; init; } = AuthenticationState.Authenticating;
    public DataSources DataSource { get; set; }
    public long TimeStamp { get; set; }
    public string? AccountDetailId { get; set; }
    public virtual IntegrationAccountDetail? AccountDetail { get; set; } = new();
}