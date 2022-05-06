using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Enums;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public record QueryModel(
    int Page,
    int RecordsPerPage,
    EntitySearchSortModel? Sort = null,
    List<SearchFilterModel>? Search = null);

public static class QueryModelExtensions
{
    public static OwnedDataSearchModel ToOwnedDataSearchModel(
        this QueryModel queryModel, 
        string businessId, 
        EntitySearchSortModel? defaultSearch = null)
    {
        OwnedDataSearchModel model = new(
            queryModel.Page,
            queryModel.RecordsPerPage,
            new List<EntitySearchSortModel>(),
            new EntitySearchModel(queryModel.Search ?? new List<SearchFilterModel>())
        )
        {
            OwnerEntityId = businessId,
            OwnerAirslipUserType = AirslipUserType.Merchant
        };

        defaultSearch = queryModel.Sort ?? defaultSearch;  
        
        if (defaultSearch != null) model.Sort.Add(defaultSearch);
        
        return model;
    }
}