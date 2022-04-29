using Airslip.Integrations.Banking.Types.Enums;
using JetBrains.Annotations;

namespace Airslip.Analytics.Reports.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record IntegrationAccountDetailReportModel
{
    public string? LastCardDigits { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public BankingUsageTypes UsageType { get; set; }
    public BankingAccountTypes AccountType { get; set; }
    public string? SortCode { get; set; }
    public string? AccountNumber { get; set; }
}