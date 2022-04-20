using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Integrations.Banking.Types.Enums;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record BankAccountMetricSnapshot : IReportableWithOwnership, IReportableWithIntegration
{
    public string Id { get; set; } = string.Empty;
    public string IntegrationId { get; set; } = string.Empty;
    public BankingAccountTypes AccountType { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public DateTime? MetricDate { get; set; } 
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
    public long TotalTransaction { get; init; }
    public int TransactionCount { get; init; }
    public long TotalCredit { get; init; }
    public int CreditCount { get; init; }
    public long TotalDebit { get; init; }
    public int DebitCount { get; init; }
}