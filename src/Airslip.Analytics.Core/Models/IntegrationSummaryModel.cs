using Airslip.Common.CustomerPortal.Enums;

namespace Airslip.Analytics.Core.Models;

public class IntegrationSummaryModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string IntegrationProviderId { get; set; } = string.Empty;
    public AuthenticationState AuthenticationState { get; set; } = AuthenticationState.Authenticating;
}