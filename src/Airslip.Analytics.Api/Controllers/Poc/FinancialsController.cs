using Airslip.Analytics.Api.Docs.Examples.Poc;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Auth.AspNetCore.Implementations;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Configuration;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Failures;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using System;

namespace Airslip.Analytics.Api.Controllers.Poc;

/// <summary>
/// A description for a group of APIs
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("v{version:apiVersion}/financials")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FinancialsController : ApiControllerBase
{
    public FinancialsController(
        ITokenDecodeService<UserToken> tokenDecodeService,
        IOptions<PublicApiSettings> publicApiOptions,
        ILogger logger) : base(tokenDecodeService,
        publicApiOptions, logger)
    {
    }

    /// <summary>
    /// The balance sheet report is a standard financial report which describes the financial position of an organisation at a point in time.
    /// </summary>
    /// <param name="balanceDate">Specifies the date for balance sheet report</param>
    /// <param name="months">The number of months to run the balance sheet for</param>
    [HttpGet("balance-sheet")]
    [ProducesResponseType(typeof(BalanceSheetModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetBalanceSheet([FromQuery] DateTimeOffset? balanceDate, [FromQuery] int months)
    {
        BalanceSheetModelExample example = new();

        IResponse response = example.GetExamples();

        return HandleResponse<BalanceSheetModel>(response);
    }
    
    /// <summary>
    /// The statement of cash flows - direct method, provides the year to date changes in operating, financing and investing cash
    /// flow activities for an organisation. Cashflow statement is not available in US region at this stage.
    /// </summary>
    /// <param name="startDate">Specifies the start date for cash flow report. If no parameter is provided, the date of 12 months before the end date will be used.</param>
    /// <param name="endDate">Specifies the end date for cash flow report. If no parameter is provided, the current date will be used.</param>
    [HttpGet("cashflow")]
    [ProducesResponseType(typeof(CashflowModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetCashflow([FromQuery] DateTimeOffset? startDate, [FromQuery] DateTimeOffset endDate)
    {
        CashflowModelExample example = new();

        IResponse response = example.GetExamples();

        return HandleResponse<CashflowModel>(response);
    }
}