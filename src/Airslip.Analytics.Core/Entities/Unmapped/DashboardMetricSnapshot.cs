using System;

namespace Airslip.Analytics.Core.Entities.Unmapped;

public class DashboardMetricSnapshot
{
    public DateTime MetricDate { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public long Balance { get; set; }
}