using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Utilities;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankAccountBalanceCreditLine : IEntityWithId
{
    public string Id { get; set; } = CommonFunctions.GetId();
    public string AccountBalanceDetailId { get; set; } = string.Empty;
    public BalanceStatus BalanceStatus { get; init; }
    public long Balance { get; init; }
    public string? Currency { get; init; }
    public CreditLineType CreditLineType { get; init; }
}