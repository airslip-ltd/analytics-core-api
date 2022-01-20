using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Utilities;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankAccountBalanceCreditLineModel : IModel
{
    public string? Id { get; set; } = CommonFunctions.GetId();
    public EntityStatus EntityStatus { get; set; }
    public BalanceStatus BalanceStatus { get; init; }
    public long Balance { get; init; }
    public string? Currency { get; init; }
    public CreditLineType CreditLineType { get; init; }
}