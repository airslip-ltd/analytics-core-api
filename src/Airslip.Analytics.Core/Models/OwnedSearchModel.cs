using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Types.Enums;
using System.ComponentModel.DataAnnotations;

namespace Airslip.Analytics.Core.Models;

public abstract record OwnedSearchModel : IOwnedSearch
{
    [Required]
    public string OwnerEntityId { get; init; } = string.Empty;
    
    [Required]
    public AirslipUserType OwnerAirslipUserType { get; init; } = AirslipUserType.Unknown;
}