using Airslip.Common.CustomerPortal.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Core.Models;

public record MerchantAccountModel : IModelWithOwnership, IFromDataSource
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string? UserId { get; set; }
    public string? EntityId { get; set; } = string.Empty;
    public AirslipUserType AirslipUserType { get; set; }
    public AuthenticationState AuthenticationState { get; init; } = AuthenticationState.Authenticating;
    public string Provider { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public DataSources DataSource { get; set; }
    public long TimeStamp { get; set; }
}