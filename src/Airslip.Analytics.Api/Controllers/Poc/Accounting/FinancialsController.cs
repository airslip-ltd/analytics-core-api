using Airslip.Analytics.Api.Docs.Examples.Poc;
using Airslip.Analytics.Reports.Models.Poc;
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
using System;

namespace Airslip.Analytics.Api.Controllers.Poc.Accounting;

/// <summary>
/// A collection of endpoints to assess the financial position of the business, which may assist lenders to gain the confidence they need to provide the financial product.
/// You can get access to balance sheets, cash flow statement, profit & loss reports and the cash position of an organisation. 
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
    
    /// <summary>
    /// The profit and loss statement is a standard financial report providing detailed year to date income and expense detail for an organisation.
    /// </summary>
    /// <param name="startDate">Specifies the start date for profit and loss report
    /// If no parameter is provided, the date of 12 months before the end date will be used.</param>
    /// <param name="endDate">Specifies the end date for profit and loss report
    /// If no parameter is provided, the current date will be used.</param>
    [HttpGet("profit-and-loss")]
    [ProducesResponseType(typeof(ProfitLossModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetProfitAndLoss([FromQuery] DateTimeOffset? startDate, [FromQuery] DateTimeOffset endDate)
    {
        ProfitLossModelExample example = new();

        IResponse response = example.GetExamples();

        return HandleResponse<ProfitLossModel>(response);
    }
    
    /// <summary>
    /// Summarizes the total cash position for each account for an org
    /// </summary>
    /// <param name="balanceDate">The `balance date` will return transactions based on the accounting date entered by the user. Transactions before the balanceDate will be included.</param>
    [HttpGet("cash-position")]
    [ProducesResponseType(typeof(CashPositionModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetCashPosition([FromQuery] DateTimeOffset? balanceDate)
    {
        CashPositionModelExample example = new();

        IResponse response = example.GetExamples();

        return HandleResponse<CashPositionModel>(response);
    }
}
