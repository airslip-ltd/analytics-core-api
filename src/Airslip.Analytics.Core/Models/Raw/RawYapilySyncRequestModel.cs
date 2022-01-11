using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Types.Enums;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Models.Raw;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class RawYapilySyncRequestModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public string YapilyAccountId { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public UsageTypes UsageType { get; set; }
    public AccountTypes AccountType { get; set; }
    public string? LastCardDigits { get; set; }
    public string FromDate { get; set; } = string.Empty;
    public string ApplicationUserId  { get; set; } = string.Empty;
    public SyncStatus SyncStatus { get; set; }
    public int RecordCount { get; set; }
    public string? TracingId { get; set; }
}