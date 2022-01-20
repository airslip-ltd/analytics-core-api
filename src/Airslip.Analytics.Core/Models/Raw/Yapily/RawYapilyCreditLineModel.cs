using Airslip.Analytics.Core.Enums;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Models.Raw.Yapily;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class RawYapilyCreditLineModel
{
    public BalanceStatus BalanceStatus { get; init; }
    public long Balance { get; init; }
    public string? Currency { get; init; }
    public CreditLineType CreditLineType { get; init; }
}