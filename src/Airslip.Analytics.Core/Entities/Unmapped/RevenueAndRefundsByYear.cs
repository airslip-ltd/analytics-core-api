using System;

namespace Airslip.Analytics.Core.Entities.Unmapped;

public class RevenueAndRefundsByYear
{
    public int Month { get; set; }
    public long TotalSales { get; set; }
    public long TotalRefunds { get; set; }
}