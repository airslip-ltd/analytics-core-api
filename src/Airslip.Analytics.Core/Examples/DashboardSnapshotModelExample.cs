using Airslip.Analytics.Core.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Examples;

public class DashboardSnapshotModelExample : IExamplesProvider<DashboardSnapshotModel>
{
    public DashboardSnapshotModel GetExamples()
    {
        return new DashboardSnapshotModel
        {
            Balance = 2,
            TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            Movement = 0.02,
            DayRange = 1,
            Metrics = new List<SnapshotMetric>
            {
                new(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), 25.3)
            }
        };
    }
}