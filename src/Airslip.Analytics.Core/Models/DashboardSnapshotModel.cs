using Airslip.Common.Types.Interfaces;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class DashboardSnapshotModel : ISuccess
{
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public double Balance { get; set; }
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public long TimeStamp { get; set; }
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public double Movement { get; set; }
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public List<SnapshotMetric> Metrics { get; set; } = new();
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public int DayRange { get; set; }
}
