using Airslip.Analytics.Core.Interfaces;
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
/// A collection of APIs for commerce transactions after a connected business links at least one of its commerce, e-commerce, POS or marketplace accounts.
/// </summary>
[ApiController]    
[ApiVersion("2021.11")]
[ApiVersion("2022.5")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("{version:apiVersion}/commerce")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CommerceController : ApiControllerBase
{
    private readonly ICommerceProviderReport _commerceProviderReport;
    private readonly IDownloadService _downloadService;
    private readonly ICommerceTransactionReport _commerceTransactionReport;

    public CommerceController(
        ICommerceProviderReport commerceProviderReport,
        IDownloadService downloadService,
        ICommerceTransactionReport commerceTransactionReport,
        ITokenDecodeService<UserToken> tokenDecodeService, 
        IOptions<PublicApiSettings> publicApiOptions, ILogger logger) 
        : base(tokenDecodeService, publicApiOptions, logger)
    {
        _commerceProviderReport = commerceProviderReport;
        _downloadService = downloadService;
        _commerceTransactionReport = commerceTransactionReport;
    }
        
    /// <summary>
    /// Get all commerce accounts for a connected business.
    /// </summary>
    /// <param name="query">The commerce transaction model within the search query, you can use this to sort or search for any column within the model.</param>
    [MapToApiVersion("2021.11")]
    [HttpPost]
    [Route("accounts/search")]
    [ProducesResponseType(typeof(EntitySearchResponse<CommerceProviderModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCommerceAccounts([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _commerceProviderReport
            .Execute(query);
            
        return HandleResponse<EntitySearchResponse<CommerceProviderModel>>(response);
    }
        
    /// <summary>
    /// Get all commerce accounts for a connected business.
    /// </summary>
    /// <param name="query">The commerce transaction model within the search query. You can use this to sort or search for any column within the model.</param>
    [MapToApiVersion("2021.11")]
    [HttpPost]
    [ProducesResponseType( typeof(DownloadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [Route("accounts/download")]
    public async Task<IActionResult> DownloadCommerceAccounts([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _downloadService.Download<BankTransactionReportModel>(_commerceProviderReport, query, 
            "commerce-providers");

        return HandleResponse<DownloadResponse>(response);
    }
    
    /// <summary>
    /// Get and search for commerce transactions for a connected business.
    /// </summary>
    /// <param name="query">The commerce transaction model within the search query. You can use this to sort or search for any column within the model.</param>
    [MapToApiVersion("2021.11")]
    [HttpPost("transactions/search")]
    [ProducesResponseType( typeof(EntitySearchResponse<CommerceTransactionReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCommerceTransactions([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _commerceTransactionReport.Execute(query);

        return HandleResponse<EntitySearchResponse<CommerceTransactionReportModel>>(response);
    }
    
    /// <summary>
    /// Download commerce transactions for a connected business.
    /// </summary>
    /// <param name="query">The commerce transaction model within the search query. You can use this to sort or search for any column within the model.</param>
    [MapToApiVersion("2021.11")]
    [HttpPost("transactions/download")]
    [ProducesResponseType( typeof(EntitySearchResponse<BankTransactionReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DownloadCommerceTransactions([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _downloadService.Download<MerchantTransactionModel>(_commerceTransactionReport, query, 
            "commerce-transactions");

        return HandleResponse<DownloadResponse>(response);
    }
    
    /// External API
    
    /// <summary>
    /// Get all commerce transactions for a connected business.
    /// </summary>
    /// <param name="query">The commerce transaction model within the search query. You can use this to sort or search for any column within the model.</param>
    /// <param name="businessId">The connected business identifier.</param>
    [MapToApiVersion("2022.5")]
    [HttpPost("{businessId}/transactions/search")]
    [ProducesResponseType( typeof(EntitySearchResponse<CommerceTransactionReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCommerceTransactions([FromRoute] string? businessId, [FromBody] QueryModel query)
    {
        OwnedDataSearchModel model = query.ToOwnedDataSearchModel(businessId, Token.EntityId);

        return await GetCommerceTransactions(model);
    }
}