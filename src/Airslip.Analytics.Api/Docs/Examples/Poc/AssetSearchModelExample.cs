using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

public class AssetSearchModelExample : IExamplesProvider<EntitySearchResponse<AssetModel>>
{
    public EntitySearchResponse<AssetModel> GetExamples()
    {
        return new EntitySearchResponse<AssetModel>
        {
            Paging = new EntitySearchPagingModel
            {
                TotalRecords = 1,
                RecordsPerPage = 10,
                Page = 1,
                Next = null
            },
            Results = new List<AssetModel>
            {
                new()
                {
                    Id = CommonFunctions.GetId(),
                    AssetName = "Computer",
                    AssetNumber = "FA-0021",
                    PurchaseDate = ExampleValues.Dates.OneYearAgo,
                    PurchasePrice = 199999,
                    DisposalPrice = 20000,
                    AssetStatus = AssetStatus.Draft,
                    AccountingBookValue = 199999,
                    BookDepreciationSetting = new()
                    {
                        DepreciationMethod = DepreciationMethods.DiminishingValue100,
                        AveragingMethod = AveragingMethods.ActualDays,
                        DepreciationRate = 40,
                        DepreciationCalculationMethod = DepreciationCalculationMethods.Rate,

                    },
                    BookDepreciationDetail = new()
                    {
                        CurrentCapitalGain = 0,
                        CurrentGainLoss = null,
                        DepreciationStartDate = ExampleValues.Dates.OneYearAgo,
                        CostLimit = 199999,
                        ResidualValue = 199999,
                        PriorAccumulativeDepreciationAmount = 0,
                        CurrentAccumulativeDepreciationAmount = 0
                    },
                }
            }
        };
    }
}