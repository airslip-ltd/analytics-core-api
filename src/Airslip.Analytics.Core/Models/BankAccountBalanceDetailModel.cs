using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Utilities;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankAccountBalanceDetailModel : IModel
{
    public string? Id { get; set; } = CommonFunctions.GetId();
    public EntityStatus EntityStatus { get; set; }
    public AccountBalanceType BalanceType { get; init; }
    public bool CreditLineIncluded { get; init; }
    public List<BankAccountBalanceCreditLineModel> CreditLines { get; init; } = new();
    public string? DateTime { get; init; }
    public BalanceStatus BalanceStatus { get; init; }
    public long Balance { get; init; }
    public string? Currency { get; init; }
}