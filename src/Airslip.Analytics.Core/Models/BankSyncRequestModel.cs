using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using Airslip.Integrations.Banking.Types.Enums;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankSyncRequestModel : IModelWithOwnership, IFromDataSource
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public string IntegrationId { get; set; } = string.Empty;
    public BankingUsageTypes UsageType { get; set; }
    public BankingAccountTypes AccountType { get; set; }
    public string? LastCardDigits { get; set; }
    public string FromDate { get; set; } = string.Empty;
    public string ApplicationUserId  { get; set; } = string.Empty;
    public BankingSyncStatus SyncStatus { get; set; }
    public int RecordCount { get; set; }
    public string? TracingId { get; set; }
    public DataSources DataSource { get; set; } = DataSources.Unknown;
    public long TimeStamp { get; set; }
}