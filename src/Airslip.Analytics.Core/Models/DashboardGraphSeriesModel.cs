using Airslip.Common.Types.Interfaces;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public record DashboardGraphSeriesModel(string StartDate, string EndDate, IEnumerable<Series> Series) : ISuccess;
