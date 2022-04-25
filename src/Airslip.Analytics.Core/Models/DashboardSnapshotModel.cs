using Airslip.Common.Types.Interfaces;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class DashboardSnapshotModel : ISuccess
{
    public double Balance { get; set; }
    public long TimeStamp { get; set; }
    public double Movement { get; set; }
    public string CurrencyCode { get; set; } = "GBP";
    public List<SnapshotMetric> Metrics { get; set; } = new();
    public int DayRange { get; set; }
}
