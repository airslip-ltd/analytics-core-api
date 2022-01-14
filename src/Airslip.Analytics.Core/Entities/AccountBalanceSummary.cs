using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities.Extensions;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record AccountBalanceSummary : IEntityWithId
{
    public string Id { get; set; } = string.Empty;
    public string? AccountId { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public DateTime UpdatedOn { get; set; }
    public long Balance { get; init; }
    public long TimeStamp { get; set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();
    public string? Currency { get; init; }
}