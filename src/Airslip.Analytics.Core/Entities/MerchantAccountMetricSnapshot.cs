using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Types.Enums;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record MerchantAccountMetricSnapshot : IReportableWithOwnership, IReportableWithIntegration
{
    public string Id { get; set; } = string.Empty;
    public string IntegrationId { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public DateTime? MetricDate { get; set; }
    public string? CurrencyCode { get; init; } = "GBP";
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
    public int OrderCount { get; init; }
    public long TotalSales { get; init; }
    public int SaleCount { get; init; }
    public long TotalRefunds { get; init; }
    public int RefundCount { get; init; }
}