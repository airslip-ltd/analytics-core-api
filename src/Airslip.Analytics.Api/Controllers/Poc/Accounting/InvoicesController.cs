using Airslip.Analytics.Api.Docs.Examples.Poc;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Auth.AspNetCore.Implementations;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Models;
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

namespace Airslip.Analytics.Api.Controllers.Poc.Accounting;

/// <summary>
/// Get, search, create and update invoices for a particular business
/// </summary>
[ApiController]
[ApiVersion("2022.5")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("{version:apiVersion}/invoices/{businessId}")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class InvoicesController : ApiControllerBase
{
    public InvoicesController(
        ITokenDecodeService<UserToken> tokenDecodeService,
        IOptions<PublicApiSettings> publicApiOptions,
        ILogger logger) : base(tokenDecodeService,
        publicApiOptions, logger)
    {
    }

    /// <summary>
    /// Use this method to retrieve one or many invoices.
    /// When you retrieve multiple invoices, only a summary of the contact is returned and no line details are returned - this is to keep the response more compact.
    /// The line item details will be returned when you retrieve an individual invoice, either by specifying Invoice ID, Invoice Number, querying by Statuses or by using the optional paging parameter (below).
    /// When you retrieve invoices by querying by Statuses, pagination is enforced by default.
    /// Individual invoices (e.g. Invoices/97c2dc5-cc47-4afd-8ec8-74990b8761e9) can also be returned as PDF's see our HTTP GET documentation
    /// </summary>
    /// <param name="businessId">The connected business identifier</param>
    /// <param name="query">The invoice model within the search query. You can use this to sort or search for any column within the model</param>
    [HttpPost("search")]
    [ProducesResponseType(typeof(EntitySearchResponse<InvoiceModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetInvoices([FromRoute] string? businessId, [FromBody] QueryModel query)
    {
        InvoiceSearchModelExample example = new();

        IResponse response = example.GetExamples();

        return HandleResponse<EntitySearchResponse<InvoiceModel>>(response);
    }

    /// <summary>
    /// Use this method to create or update an invoice
    /// </summary>
    /// <param name="businessId">The connected business identifier</param>
    /// <param name="body">The body of the invoice to update or create. You can use this to sort or search for any column within the model</param>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult CreateInvoice([FromRoute] string businessId, [FromBody] InvoiceModel body)
    {
        return HandleResponse<CreatedModel>(body);
    }
}