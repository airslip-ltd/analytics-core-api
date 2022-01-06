using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace Airslip.Bootstrap.Function.Functions
{
    public static class MethodWithoutAuthorisation
    {
        [OpenApiOperation(operationId: "MethodWithoutAuthorisation")]
        [Function(nameof(MethodWithoutAuthorisation))]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/request/without/auth")] HttpRequestData req,
            FunctionContext executionContext)
        {
            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}