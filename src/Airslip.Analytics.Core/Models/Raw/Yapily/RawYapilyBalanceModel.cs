using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Types.Enums;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models.Raw.Yapily;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record RawYapilyBalanceModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public string AccountId { get; set; } = string.Empty;
    public string ApplicationUserId { get; set; } = string.Empty;
    public string YapilyConsentId { get; set; } = string.Empty;
    public BalanceStatus BalanceStatus { get; init; }
    public long Balance { get; init; }
    public string? Currency { get; init; }
    public List<RawYapilyBalanceDetailModel> Details { get; init; } = new();
    public long TimeStamp { get; set; }
}