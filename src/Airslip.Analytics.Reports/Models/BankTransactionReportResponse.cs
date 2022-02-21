using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Reports.Models;

public class BankTransactionReportResponse : IModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string BankTransactionId { get; set; } = string.Empty;
    public string? TransactionHash { get; set; }
    public string AccountId { get; set; } = string.Empty;
    public string BankId { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public long? AuthorisedDate { get; set; }
    public long CapturedDate { get; set; }
    public long Amount { get; set; }
    public string? CurrencyCode { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? AddressLine { get; set; }
    public string? LastCardDigits { get; set; }
    public string? IsoFamilyCode { get; set; }
    public string? ProprietaryCode { get; set; }
    public string? TransactionIdentifier { get; set; }
    public string? Reference { get; set; }
    public DataSources DataSource { get; set; }
    public long TimeStamp { get; set; }
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
    public string TradingName { get; set; } = string.Empty;
}