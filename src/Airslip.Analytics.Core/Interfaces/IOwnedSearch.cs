using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Core.Interfaces;

public interface IOwnedSearch
{
    string OwnerEntityId { get; }
    
    AirslipUserType OwnerAirslipUserType { get; }
}