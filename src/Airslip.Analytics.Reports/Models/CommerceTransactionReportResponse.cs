using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Reports.Models;

public class CommerceTransactionReportResponse : IModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public DataSources DataSource { get; set; }
    public long TimeStamp { get; set; }
    public string TrackingId { get; set; } = string.Empty;
    public string? InternalId { get; init; }
    public string? Source { get; init; }
    public string? TransactionNumber { get; init; }
    public string? RefundCode { get; init; }
    public DateTime? Datetime { get; init; }
    public string? StoreLocationId { get; init; }
    public string? StoreAddress { get; init; }
    public bool? OnlinePurchase { get; init; }
    public long? Subtotal { get; init; }
    public long? ServiceCharge { get; init; }
    public long? Total { get; init; }
    public string? CurrencyCode { get; init; }
    public string? CustomerEmail { get; init; }
    public string? OperatorName { get; init; }
    public DateTime? Date { get; init; }
    public string? Time { get; init; }
    public string? Till { get; init; }
    public string? Number { get; init; }
    public string? Store { get; init; }
    public string? Description { get; init; }
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
    public long? TotalRefund { get; set; }
    public string OrderStatus { get; init; } = string.Empty;
    public string PaymentStatus { get; init; } = string.Empty;
}