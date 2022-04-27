using Airslip.Analytics.Core.Data;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities.Extensions;
using Airslip.Integrations.Banking.Types.Enums;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record BankBusinessBalanceSnapshot : IReportableWithCurrency
{
    public string Id { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public DateTime UpdatedOn { get; set; }
    public long Balance { get; init; }
    public BankingAccountTypes AccountType { get; set; }
    public long TimeStamp { get; set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();
    public string CurrencyCode { get; init; } = Constants.DEFAULT_CURRENCY;
}
