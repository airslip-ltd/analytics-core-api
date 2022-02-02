using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public record TimelyMetric(int Period, string Description, long Balance, PeriodType PeriodType);

public record Series(string Name, IEnumerable<TimelyMetric> Metrics, IEnumerable<long> Data);