using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.CustomerPortal.Enums;

namespace Airslip.Analytics.Core.Models;

public record MerchantAccountSummaryModel : IModelWithId
{
    public string Id { get; set; } = string.Empty;
    public AuthenticationState AuthenticationState { get; init; } = AuthenticationState.Authenticating;
    public string Provider { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}