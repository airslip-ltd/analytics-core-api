using System;

namespace Airslip.Analytics.Core.Entities.Unmapped;

public class DebitsAndCreditsByYear
{
    public int Month { get; set; }
    public long TotalDebit { get; set; }
    public long TotalCredit { get; set; }
}