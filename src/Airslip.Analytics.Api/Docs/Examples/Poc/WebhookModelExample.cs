using Airslip.Analytics.Core.Poc;
using Swashbuckle.AspNetCore.Filters;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

public class WebhookModelExample : IExamplesProvider<WebhookModel>
{
    public WebhookModel GetExamples()
    {
        return new WebhookModel
        {
        };
    }
}