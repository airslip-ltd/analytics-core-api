using Airslip.Analytics.Core.Enums;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record IntegrationAccountDetailModel
{
    public string Id { get; set; } = string.Empty;
    public AccountStatus AccountStatus { get; set; }
    public string AccountId { get; set; } = string.Empty;
    public string? LastCardDigits { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string UsageType { get; set; } = string.Empty;
    public string AccountType { get; set; } = string.Empty;
    public string? SortCode { get; set; }
    public string? AccountNumber { get; set; }
}