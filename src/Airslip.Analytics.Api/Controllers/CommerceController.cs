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

[ApiController]    
[ApiVersion("1.0")]
[Produces(Json.MediaType)]
[Route("v{version:apiVersion}/commerce")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CommerceController : ApiControllerBase
{
    private readonly ICommerceProviderReport _commerceProviderReport;
    private readonly IDownloadService _downloadService;

    public CommerceController(ICommerceProviderReport commerceProviderReport,
        IDownloadService downloadService,
        ITokenDecodeService<UserToken> tokenDecodeService, 
        IOptions<PublicApiSettings> publicApiOptions, ILogger logger) 
        : base(tokenDecodeService, publicApiOptions, logger)
    {
        _commerceProviderReport = commerceProviderReport;
        _downloadService = downloadService;
    }
        
    [HttpPost]
    [Route("search")]
    [ProducesResponseType(typeof(EntitySearchResponse<CommerceProviderModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProviders([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _commerceProviderReport
            .Execute(query);
            
        return HandleResponse<EntitySearchResponse<CommerceProviderModel>>(response);
    }
        
    [HttpPost]
    [ProducesResponseType( typeof(DownloadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [Route("download")]
    public async Task<IActionResult> BankTransactionsDownload([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _downloadService.Download<BankTransactionReportModel>(_commerceProviderReport, query, 
            "commerce-providers");

        return HandleResponse<DownloadResponse>(response);
    }
}