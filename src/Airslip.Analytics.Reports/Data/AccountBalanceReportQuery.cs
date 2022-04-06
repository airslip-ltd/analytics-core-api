using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Reports.Interfaces;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Integrations.Banking.Types.Enums;

namespace Airslip.Analytics.Reports.Data;

public class AccountBalanceReportQuery : IEntity, IOwnedDataQuery
{
    public string Id { get; set; } = string.Empty;
    public virtual BasicAuditInformation? AuditInformation { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string IntegrationProviderId { get; set; } = string.Empty;
    public BankingAccountStatus AccountStatus { get; set; }
    public string? SortCode { get; set; }
    public string? AccountNumber { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public double Balance { get; set; }
    public DateTime UpdatedOn { get; set; }
    
    public string OwnerEntityId { get; init; }
    public AirslipUserType OwnerAirslipUserType { get; init; }
    public string ViewerEntityId { get; init; } = string.Empty;
    public AirslipUserType ViewerAirslipUserType { get; init; }
    public string PermissionType { get; init; } = string.Empty;
    public bool Allowed { get; init; }
}
