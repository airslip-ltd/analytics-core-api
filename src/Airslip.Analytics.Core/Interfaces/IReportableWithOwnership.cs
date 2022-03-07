using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Core.Interfaces;

public interface IReportableWithOwnership : IEntityWithId
{
    string? EntityId { get; set; }
    AirslipUserType AirslipUserType { get; set; }
}

public interface IReportableWithAccount
{
    string AccountId { get; set; }
}