using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities.Extensions;
using Airslip.Integrations.Banking.Types.Enums;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record BankAccountBalanceSummary : IReportableWithCurrency, IReportableWithIntegration
{
    public string Id { get; set; } = string.Empty;
    public string IntegrationId { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public DateTime UpdatedOn { get; set; }
    public BankingAccountTypes AccountType { get; set; }
    public long Balance { get; init; }
    public double Movement { get; set; }
    public long TimeStamp { get; set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();
    public string? Currency { get; init; }
}