using Airslip.Common.Types.Interfaces;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public record DashboardGraphSeriesModel(int Year, IEnumerable<Series> Series) : ISuccess;