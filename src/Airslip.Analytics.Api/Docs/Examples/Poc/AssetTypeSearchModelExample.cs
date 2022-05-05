using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

public class AssetTypeSearchModelExample : IExamplesProvider<EntitySearchResponse<AssetTypeModel>>
{
    public EntitySearchResponse<AssetTypeModel> GetExamples()
    {
        return new EntitySearchResponse<AssetTypeModel>
        {
            Paging = new EntitySearchPagingModel
            {
                TotalRecords = 1,
                RecordsPerPage = 10,
                Page = 1,
                Next = null
            },
            Results = new List<AssetTypeModel>
            {
                new()
                {
                    Name = "Laptop ProV1 2022",
                    FixedAssetAccountId = CommonFunctions.GetId(),
                    DepreciationExpenseAccountId = CommonFunctions.GetId(),
                    AccumulatedDepreciationAccountId = CommonFunctions.GetId(),
                    BookDepreciationSetting = new()
                    {
                        DepreciationMethod = DepreciationMethods.DiminishingValue100,
                        AveragingMethod = AveragingMethods.ActualDays,
                        DepreciationRate = 40,
                        DepreciationCalculationMethod = DepreciationCalculationMethods.Rate,
                    },
                    Id = CommonFunctions.GetId(),
                }
            }
        };
    }
}