using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Enums;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public record QueryModel(
    int Page,
    int RecordsPerPage,
    EntitySearchSortModel Sort,
    List<SearchFilterModel> Search);

public static class QueryModelExtensions
{
    public static OwnedDataSearchModel ToOwnedDataSearchModel(
        this QueryModel queryModel, 
        string? businessId, 
        string entityId)
    {
        OwnedDataSearchModel model = new(
            queryModel.Page,
            queryModel.RecordsPerPage,
            new List<EntitySearchSortModel>(),
            new EntitySearchModel(queryModel.Search)
        )
        {
            OwnerEntityId = businessId ?? entityId,
            OwnerAirslipUserType = AirslipUserType.Merchant
        };
        
        if (queryModel.Sort != null) model.Sort.Add(queryModel.Sort);
        
        return model;
    }
}