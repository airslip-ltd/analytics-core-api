namespace Airslip.Analytics.Core.Models;

public record BankTransactionSummaryModel(string Id, string InstitutionId, double Amount, 
    string CurrencyCode, long CapturedDate, string Description);