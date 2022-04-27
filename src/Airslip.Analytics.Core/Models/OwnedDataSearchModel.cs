using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Airslip.Analytics.Core.Data;

namespace Airslip.Analytics.Core.Models;

public record OwnedDataSearchModel : EntitySearchQueryModel, IOwnedSearch
{
    public OwnedDataSearchModel(int Page, int RecordsPerPage,  
        List<EntitySearchSortModel> Sort, EntitySearchModel? Search) 
        : base(Page, RecordsPerPage, Sort, Search)
    {
        
    }
    
    [Required]
    public string OwnerEntityId { get; init; } = string.Empty;
    
    [Required]
    public AirslipUserType OwnerAirslipUserType { get; init; } = AirslipUserType.Unknown;
}