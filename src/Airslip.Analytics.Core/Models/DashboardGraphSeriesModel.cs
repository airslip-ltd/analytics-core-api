using Airslip.Common.Types.Interfaces;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public record DashboardGraphSeriesModel(string StartDate, string EndDate, string CurrencyCode, IEnumerable<Series> Series) : ISuccess;
