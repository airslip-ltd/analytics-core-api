using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Utilities;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankAccountBalanceDetail : IEntityWithId
{
    public string Id { get; set; } = CommonFunctions.GetId();
    public string AccountBalanceId { get; set; } = string.Empty;
    public AccountBalanceType BalanceType { get; init; }
    public bool CreditLineIncluded { get; init; }
    public virtual ICollection<BankAccountBalanceCreditLine> CreditLines { get; init; } = new List<BankAccountBalanceCreditLine>();
    public string? DateTime { get; init; }
    public BalanceStatus BalanceStatus { get; init; }
    public long Balance { get; init; }
    public string? Currency { get; init; }
}