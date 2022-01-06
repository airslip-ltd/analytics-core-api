using Airslip.Common.Auth.Data;
using Airslip.Common.Auth.Functions.Attributes;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace Airslip.Analytics.Processor.Functions
{
    public static class MethodWithAuthorisation
    {
        [OpenApiOperation(operationId: "MethodWithAuthorisation", Summary = "Do something using your Api Key")]
        [OpenApiSecurity(AirslipSchemeOptions.ApiKeyScheme, SecuritySchemeType.ApiKey, 
            Name = AirslipSchemeOptions.ApiKeyHeaderField, In = OpenApiSecurityLocationType.Header)]
        [Function(nameof(MethodWithAuthorisation))]
        [ApiKeyAuthorize]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/request/with/auth")] HttpRequestData req,
            FunctionContext executionContext)
        {
            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}