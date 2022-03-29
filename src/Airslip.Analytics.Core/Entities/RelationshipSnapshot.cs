using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Types.Enums;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record RelationshipSnapshot : IReportableWithOwnership
{
    public string Id { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public DateTime? MetricDate { get; set; } 
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
    public int InvitedCount { get; init; }
    public int AcceptedCount { get; init; }
    public int DeclinedCount { get; init; }
}