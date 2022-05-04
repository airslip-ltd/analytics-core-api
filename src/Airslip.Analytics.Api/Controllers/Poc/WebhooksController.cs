using Airslip.Analytics.Api.Docs.Examples.Poc;
using Airslip.Analytics.Core.Poc;
using Airslip.Common.Auth.AspNetCore.Implementations;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Types.Configuration;
using Airslip.Common.Types.Failures;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace Airslip.Analytics.Api.Controllers.Poc;

/// <summary>
/// You can configure webhook endpoints via the API to be notified about events that happen in your for connected businesses.
/// Most users configure webhooks from the dashboard, which provides a user interface for registering and testing your webhook endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("v{version:apiVersion}/web-hooks")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WebhooksController : ApiControllerBase
{
    public WebhooksController(
        ITokenDecodeService<UserToken> tokenDecodeService,
        IOptions<PublicApiSettings> publicApiOptions,
        ILogger logger) : base(tokenDecodeService,
        publicApiOptions, logger)
    {
    }

    /// <summary>
    /// Create a webhook so you can get notified when a new event has occured
    /// </summary>
    /// <param name="model"></param>
    [HttpPost]
    [ProducesResponseType(typeof(WebhookModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult CreateWebhook([FromBody] WebhookModel model)
    {
        if (model.EnabledEvents.Contains(WebhookEvents.None))
            return BadRequest(new InvalidResource(nameof(model.EnabledEvents), "Must be a valid event"));
        
        WebhookModelExample example = new();

        IResponse response = example.GetExamples();

        return HandleResponse<WebhookModel>(response);
    }
}