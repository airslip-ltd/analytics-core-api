using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Interfaces;
using Airslip.Analytics.Reports.Models;
using Airslip.Common.Auth.AspNetCore.Implementations;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Configuration;
using Airslip.Common.Types.Enums;
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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airslip.Analytics.Api.Controllers;

/// <summary>
/// A collection of APIs for bank account balances after a connected business links at least one of their bank accounts
/// </summary>
[ApiController]
[ApiVersion("2021.11")]
[ApiVersion("2022.5")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("{version:apiVersion}/banking")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BalancesController : ApiControllerBase
{
    private readonly IAccountBalanceReport _accountBalanceReport;
    private readonly IDownloadService _downloadService;

    public BalancesController(IAccountBalanceReport accountBalanceReport,
        IDownloadService downloadService,
        ITokenDecodeService<UserToken> tokenDecodeService,
        IOptions<PublicApiSettings> publicApiOptions, ILogger logger)
        : base(tokenDecodeService, publicApiOptions, logger)
    {
        _accountBalanceReport = accountBalanceReport;
        _downloadService = downloadService;
    }

    
    /// <summary>
    /// Search for balances for a connected business
    /// </summary>
    /// <param name="query">The account balance model within the search query. You can use this to sort or search for any column within the model</param>
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
    /// Download balances for a connected business
    /// </summary>
    /// <param name="query">The download account balance model within the search query. You can use this to sort or search for any column within the model</param>
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
    
    /// <summary>
    /// Search for balances for a connected business
    /// </summary>
    /// <param name="query">The account balance model within the search query. You can use this to sort or search for any column within the model</param>
    /// <param name="businessId">The connected business identifier</param>
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
