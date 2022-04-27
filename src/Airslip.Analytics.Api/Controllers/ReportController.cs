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
/// A description for a group of APIs
/// </summary>
[ApiController]    
[ApiVersion("1.0")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("v{version:apiVersion}/reports")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReportController : ApiControllerBase
{
    private readonly IBankTransactionReport _bankTransactionReport;
    private readonly ICommerceTransactionReport _commerceTransactionReport;
    private readonly IDownloadService _downloadService;

    public ReportController(ITokenDecodeService<UserToken> tokenDecodeService, 
        IBankTransactionReport bankTransactionReport,
        ICommerceTransactionReport commerceTransactionReport,
        IDownloadService downloadService,
        IOptions<PublicApiSettings> publicApiOptions, ILogger logger) : base(tokenDecodeService, 
        publicApiOptions, logger)
    {
        _bankTransactionReport = bankTransactionReport;
        _commerceTransactionReport = commerceTransactionReport;
        _downloadService = downloadService;
    }
    
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    /// <param name="query">A parameter description should go here</param>
    [HttpPost]
    [ProducesResponseType(typeof(EntitySearchResponse<BankTransactionReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [Route("bank-transactions")]
    public async Task<IActionResult> GetBankTransactions([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _bankTransactionReport.Execute(query);

        return HandleResponse<EntitySearchResponse<BankTransactionReportModel>>(response);
    }
    
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BankTransactionReportModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetBankTransaction([FromRoute] string id)
    {
        BankTransactionReportModelExample example = new();
        
        IResponse response = example.GetExamples();
            
        return HandleResponse<BankTransactionReportModel>(response);
    }
    
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    /// <param name="query">A parameter description should go here</param>
    [HttpPost]
    [ProducesResponseType( typeof(EntitySearchResponse<BankTransactionReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [Route("bank-transactions/download")]
    public async Task<IActionResult> DownloadBankTransactions([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _downloadService.Download<BankTransactionReportModel>(_bankTransactionReport, query, 
            "bank-transactions");

        return HandleResponse<DownloadResponse>(response);
    }
    
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    /// <param name="query">A parameter description should go here</param>
    [HttpPost]
    [ProducesResponseType( typeof(EntitySearchResponse<CommerceTransactionReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [Route("commerce-transactions")]
    public async Task<IActionResult> CommerceTransactions([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _commerceTransactionReport.Execute(query);

        return HandleResponse<EntitySearchResponse<CommerceTransactionReportModel>>(response);
    }
    
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    /// <param name="query">A parameter description should go here</param>
    [HttpPost]
    [ProducesResponseType( typeof(EntitySearchResponse<BankTransactionReportModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [Route("commerce-transactions/download")]
    public async Task<IActionResult> CommerceTransactionsDownload([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _downloadService.Download<MerchantTransactionModel>(_commerceTransactionReport, query, 
            "commerce-transactions");

        return HandleResponse<DownloadResponse>(response);
    }
}