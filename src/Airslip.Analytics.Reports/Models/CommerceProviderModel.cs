using Airslip.Common.CustomerPortal.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models;

public record CommerceProviderModel : IModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string IntegrationProviderId { get; init; } = string.Empty;
    public AuthenticationState AuthenticationState { get; init; } = AuthenticationState.Authenticating;
    public string Name { get; init; } = string.Empty;
    public long TimeStamp { get; set; }
}