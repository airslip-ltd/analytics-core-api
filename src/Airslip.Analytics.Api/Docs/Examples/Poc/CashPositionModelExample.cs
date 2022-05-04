using Airslip.Analytics.Reports.Enums;
using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

public class CashPositionModelExample : IExamplesProvider<CashPositionModel>
{
    public CashPositionModel GetExamples()
    {
        return new CashPositionModel
        {
            
        };
    }
}