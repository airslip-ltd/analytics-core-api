using Airslip.Common.Types.Interfaces;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class CurrentSnapshotModel : ISuccess
{
    public long Balance { get; set; }
    public long TimeStamp { get; set; }
    public double Movement { get; set; }

    public List<SnapshotMetric> Metrics { get; set; } = new();
}