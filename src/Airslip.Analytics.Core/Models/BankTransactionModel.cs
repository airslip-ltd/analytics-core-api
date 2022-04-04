using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankTransactionModel : IModelWithOwnership, IFromDataSource, ITraceable
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public string IntegrationId { get; set; } = string.Empty;
    public string BankTransactionId { get; set; } = string.Empty;
    public string? TransactionHash { get; set; }
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
    public DataSources DataSource { get; set; } = DataSources.Unknown;
    public long TimeStamp { get; set; }
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
    public string TraceInfo => $"Id: {Id}, EntityId: {EntityId}, AirslipUserType: {AirslipUserType}, IntegrationId: {IntegrationId}";
}