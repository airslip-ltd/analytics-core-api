namespace Airslip.Analytics.Core.Models;

public record TransactionSummaryModel(string Id, string Source, double Amount, 
    string CurrencyCode, long CapturedDate, string Description);