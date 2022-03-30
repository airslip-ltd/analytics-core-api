using Airslip.Analytics.Reports.Interfaces;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities.Extensions;

namespace Airslip.Analytics.Reports.Data;

public class CommerceTransactionReportQuery : IEntity, IOwnedDataQuery
{
    public string Id { get; set; } = string.Empty;
    public virtual BasicAuditInformation? AuditInformation { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string? UserId { get; set; }
    public string? IntegrationId { get; set; }
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
    
    public string OwnerEntityId { get; init; }
    public AirslipUserType OwnerAirslipUserType { get; init; }
    public string ViewerEntityId { get; init; }
    public AirslipUserType ViewerAirslipUserType { get; init; }
    public string PermissionType { get; init; }
    public bool Allowed { get; init; }
    public string OrderStatus { get; init; }
    public string PaymentStatus { get; init; }
    public long? TotalRefund { get; set; }
}