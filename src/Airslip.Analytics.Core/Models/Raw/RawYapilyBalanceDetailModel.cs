using Airslip.Analytics.Core.Enums;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models.Raw;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class RawYapilyBalanceDetailModel
{
    public AccountBalanceType BalanceType { get; init; }
    public bool CreditLineIncluded { get; init; }
    public List<RawYapilyCreditLineModel> CreditLines { get; init; } = new();
    public string? DateTime { get; init; }
    public BalanceStatus BalanceStatus { get; init; }
    public long Balance { get; init; }
    public string? Currency { get; init; }
}