using Airslip.Analytics.Core.Enums;
using Airslip.Integrations.Banking.Types.Enums;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record IntegrationAccountDetailModel
{
    public string Id { get; set; } = string.Empty;
    public BankingAccountStatus AccountStatus { get; set; }
    public string AccountId { get; set; } = string.Empty;
    public string? LastCardDigits { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public BankingUsageTypes UsageType { get; set; }
    public BankingAccountTypes AccountType { get; set; }
    public string? SortCode { get; set; }
    public string? AccountNumber { get; set; }
}