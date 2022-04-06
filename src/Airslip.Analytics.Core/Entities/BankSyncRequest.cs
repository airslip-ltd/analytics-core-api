using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities.Extensions;
using Airslip.Integrations.Banking.Types.Enums;
using JetBrains.Annotations;
using System;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankSyncRequest : IEntityWithOwnership, IFromDataSource, IReportableWithIntegration
{
    public string Id { get; set; } = string.Empty;
    public virtual BasicAuditInformation? AuditInformation { get; set; }
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
    public long TimeStamp { get; set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();
}