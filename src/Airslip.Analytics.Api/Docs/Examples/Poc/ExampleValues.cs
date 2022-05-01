using Airslip.Common.Utilities.Extensions;
using System;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

public static class ExampleValues
{
    public static class Dates
    {
        public static long Now => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        public static long Today => DateTime.Today.ToUnixTimeMilliseconds();
        public static long ThirtyDaysAgo => DateTime.Today.AddDays(-30).ToUnixTimeMilliseconds();
        public static long OneYearAgo => DateTime.Today.AddYears(-1).AddDays(1).ToUnixTimeMilliseconds();
    }
}