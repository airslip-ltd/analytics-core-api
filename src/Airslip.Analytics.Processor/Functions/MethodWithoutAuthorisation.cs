using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Airslip.Analytics.Processor.Functions
{
    public static class MethodWithoutAuthorisation
    {
        [OpenApiOperation(operationId: "MethodWithoutAuthorisation")]
        [Function(nameof(MethodWithoutAuthorisation))]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/request/without/auth")] HttpRequestData req,
            FunctionContext executionContext)
        {
            IRepository<MyEntity, MyModel> repo = executionContext
                .InstanceServices.GetService<IRepository<MyEntity, MyModel>>() ?? throw new NotImplementedException();
            
            IEntitySearch<MyEntity, MyModel> search = executionContext
                .InstanceServices.GetService<IEntitySearch<MyEntity, MyModel>>() ?? throw new NotImplementedException();

            MyModel model = new()
            {
                Id = CommonFunctions.GetId(),
                EntityId = "im-an-entity",
                UserId = "Im a user",
                AirslipUserType = AirslipUserType.Standard
            };

            await repo.Add(model, "this-user-id");

            List<MyModel> result = await search.GetSearchResults(new List<SearchFilterModel>()
            {
                new("EntityId", model.EntityId)
            });
            
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(result);

            return response;
        }
    }
}