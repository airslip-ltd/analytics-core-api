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
/// A description for a group of APIs
/// </summary>
[ApiController]    
[ApiVersion("1.0")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("v{version:apiVersion}/balance")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AccountsController : ApiControllerBase
{
    private readonly IAccountBalanceReport _accountBalanceReport;
    private readonly IDownloadService _downloadService;

    public AccountsController(IAccountBalanceReport accountBalanceReport,
        IDownloadService downloadService, 
        ITokenDecodeService<UserToken> tokenDecodeService, 
        IOptions<PublicApiSettings> publicApiOptions, ILogger logger) 
        : base(tokenDecodeService, publicApiOptions, logger)
    {
        _accountBalanceReport = accountBalanceReport;
        _downloadService = downloadService;
    }
        
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    /// <param name="query">A parameter description should go here</param>
    [HttpPost]
    [Route("search")]
    [ProducesResponseType(typeof(EntitySearchResponse<AccountBalanceReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAccountBalances([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _accountBalanceReport
            .Execute(query);
            
        return HandleResponse<EntitySearchResponse<AccountBalanceReportModel>>(response);
    }
    
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    /// <param name="query">A parameter description should go here</param>
    [HttpPost]
    [Route("search/{businessId}")]
    [ProducesResponseType(typeof(EntitySearchResponse<AccountBalanceReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAccountBalances([FromBody] QueryModel queryModel, [FromRoute] string businessId)
    {
        OwnedDataSearchModel model = new(queryModel.Page, queryModel.RecordsPerPage,
            new List<EntitySearchSortModel>
            {
                queryModel.Sort
            },
            new EntitySearchModel(queryModel.Search))
        {
            OwnerEntityId = businessId,
            OwnerAirslipUserType = AirslipUserType.Merchant
        };

        return await GetAccountBalances(model);
    }
        
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    /// <param name="query">A parameter description should go here</param>
    [HttpPost]
    [ProducesResponseType( typeof(DownloadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [Route("download")]
    public async Task<IActionResult> DownloadAccountBalances([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _downloadService.Download<BankTransactionReportModel>(_accountBalanceReport, query, 
            "account-balances");

        return HandleResponse<DownloadResponse>(response);
    }
}