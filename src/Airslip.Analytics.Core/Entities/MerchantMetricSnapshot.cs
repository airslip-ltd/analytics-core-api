using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities.Extensions;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record MerchantMetricSnapshot : IEntityWithId
{
    public string Id { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public DateTime? MetricDate { get; set; } 
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
    public int OrderCount { get; init; }
    public long TotalSales { get; init; }
    public int SaleCount { get; init; }
    public long TotalRefunds { get; init; }
    public int RefundCount { get; init; }
}