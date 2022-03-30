using Airslip.Common.Types.Enums;
using System.Diagnostics;

namespace Airslip.Analytics.Reports.Interfaces;

public interface IOwnedDataQuery
{
    string OwnerEntityId { get; init; }
    AirslipUserType OwnerAirslipUserType { get; init; }
    string ViewerEntityId { get; init; }
    AirslipUserType ViewerAirslipUserType { get; init; }
    string PermissionType { get; init; }
    bool Allowed { get; init; }
}