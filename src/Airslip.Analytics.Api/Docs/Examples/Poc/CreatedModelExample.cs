using Airslip.Analytics.Api.Controllers.Poc;
using Airslip.Analytics.Reports.Models.Poc;
using Swashbuckle.AspNetCore.Filters;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

public class CreatedModelExample : IExamplesProvider<CreatedModel>
{
    public CreatedModel GetExamples()
    {
        return new CreatedModel
        {
            Id = "12b14d2970984e359e8bf4dc9af7812b"
        };
    }
}