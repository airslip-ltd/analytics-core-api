using Airslip.Common.Types.Interfaces;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public record RevenueAndRefundsByYearModel(int Year, IEnumerable<Series> Series) : ISuccess;