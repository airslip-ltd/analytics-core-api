using Airslip.Analytics.Api.Docs.Examples.Poc;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Interfaces;
using Airslip.Analytics.Reports.Models;
using Airslip.Common.Auth.AspNetCore.Implementations;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Configuration;
using Airslip.Common.Types.Failures;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Types.Responses;
using Airslip.Common.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using System.Threading.Tasks;

namespace Airslip.Analytics.Api.Controllers;

/// <summary>
/// A collection of banking APIs for a connected business.
/// You can use this API to retrieve bank transactions, categorised line by line merchant details, director information and account balances.
/// </summary>
[ApiController]
[ApiVersion("2021.11")]
[ApiVersion("2022.5")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("{version:apiVersion}/banking")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BankingController : ApiControllerBase
{
    private readonly IBankTransactionReport _bankTransactionReport;
    private readonly IDownloadService _downloadService;
    private readonly IAccountBalanceReport _accountBalanceReport;

    public BankingController(
        ITokenDecodeService<UserToken> tokenDecodeService,
        IBankTransactionReport bankTransactionReport,
        IDownloadService downloadService,
        IOptions<PublicApiSettings> publicApiOptions,
        ILogger logger, IAccountBalanceReport accountBalanceReport) :
        base(tokenDecodeService,
            publicApiOptions, logger)
    {
        _bankTransactionReport = bankTransactionReport;
        _downloadService = downloadService;
        _accountBalanceReport = accountBalanceReport;
    }

    /// <summary>
    /// Get bank transactions enriched with merchant and director details for a connected business.
    /// </summary>
    /// <param name="query">The bank transaction model within the search query. You can use this to sort or search for any column within the model.</param>
    [MapToApiVersion("2021.11")]
    [HttpPost("transactions/search")]
    [ProducesResponseType(typeof(EntitySearchResponse<BankTransactionReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBankTransactions([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _bankTransactionReport.Execute(query);

        return HandleResponse<EntitySearchResponse<BankTransactionReportModel>>(response);
    }

    /// <summary>
    /// Get bank transactions enriched with merchant and director details for a connected business.
    /// </summary>
    /// <param name="query">The bank transaction model within the search query. You can use this to sort or search for any column within the model.</param>
    [MapToApiVersion("2021.11")]
    [HttpPost("transactions/download")]
    [ProducesResponseType(typeof(EntitySearchResponse<BankTransactionReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DownloadBankTransactions([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _downloadService.Download<BankTransactionReportModel>(_bankTransactionReport, query,
            "bank-transactions");

        return HandleResponse<DownloadResponse>(response);
    }
    
    /// <summary>
    /// Search for balances for a connected business.
    /// </summary>
    /// <param name="query">The account balance model within the search query. You can use this to sort or search for any column within the model.</param>
    [MapToApiVersion("2021.11")]
    [HttpPost("balances/search")]
    [ProducesResponseType(typeof(EntitySearchResponse<AccountBalanceReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAccountBalances([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _accountBalanceReport
            .Execute(query);

        return HandleResponse<EntitySearchResponse<AccountBalanceReportModel>>(response);
    }

    /// <summary>
    /// Download balances for a connected business.
    /// </summary>
    /// <param name="query">The download account balance model within the search query. You can use this to sort or search for any column within the model.</param>
    [MapToApiVersion("2021.5")]
    [HttpPost("balances/download")]
    [ProducesResponseType(typeof(DownloadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DownloadAccountBalances([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _downloadService.Download<BankTransactionReportModel>(
            _accountBalanceReport,
            query,
            "account-balances");

        return HandleResponse<DownloadResponse>(response);
    }

    /// External API
    /// <summary>
    /// Search for bank transactions for a connected business.
    /// </summary>
    /// <param name="query">The bank transaction model within the search query. You can use this to sort or search for any column within the model.</param>
    /// <param name="businessId">The connected business identifier.</param>
    [MapToApiVersion("2022.5")]
    [HttpPost("{businessId}/transactions/search")]
    [ProducesResponseType(typeof(EntitySearchResponse<BankTransactionReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBankTransactions([FromRoute] string? businessId, [FromBody] QueryModel query)
    {
        OwnedDataSearchModel model = query.ToOwnedDataSearchModel(businessId, Token.EntityId);

        return await GetBankTransactions(model);
    }

    /// <summary>
    /// Get a singular bank transaction by id.
    /// </summary>
    /// <param name="businessId">The connected business identifier.</param>
    /// <param name="id">The identifier for the bank transaction.</param>
    [MapToApiVersion("2022.5")]
    [HttpGet("{businessId}/transactions/{id}")]
    [ProducesResponseType(typeof(BankTransactionReportModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetBankTransaction([FromRoute] string? businessId, [FromRoute] string id)
    {
        BankTransactionReportModelExample example = new();

        IResponse response = example.GetExamples();

        return HandleResponse<BankTransactionReportModel>(response);
    }

    /// <summary>
    /// Search for balances for a connected business.
    /// </summary>
    /// <param name="query">The account balance model within the search query. You can use this to sort or search for any column within the model.</param>
    /// <param name="businessId">The connected business identifier.</param>
    [MapToApiVersion("2022.5")]
    [HttpPost("{businessId}/balances/search")]
    [ProducesResponseType(typeof(EntitySearchResponse<AccountBalanceReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAccountBalances([FromRoute] string? businessId, [FromBody] QueryModel query)
    {
        OwnedDataSearchModel model = query.ToOwnedDataSearchModel(businessId, Token.EntityId);

        return await GetAccountBalances(model);
    }
}