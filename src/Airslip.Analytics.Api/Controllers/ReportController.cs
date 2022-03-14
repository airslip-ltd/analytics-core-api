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
[Produces(Common.Utilities.Json.MediaType)]
[Route("v{version:apiVersion}/reports")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReportController : ApiControllerBase
{
    private readonly IBankTransactionReport _bankTransactionReport;
    private readonly ICommerceTransactionReport _commerceTransactionReport;

    public ReportController(ITokenDecodeService<UserToken> tokenDecodeService, 
        IBankTransactionReport bankTransactionReport,
        ICommerceTransactionReport commerceTransactionReport,
        IOptions<PublicApiSettings> publicApiOptions, ILogger logger) : base(tokenDecodeService, 
        publicApiOptions, logger)
    {
        _bankTransactionReport = bankTransactionReport;
        _commerceTransactionReport = commerceTransactionReport;
    }
    
    [HttpPost]
    [ProducesResponseType( typeof(EntitySearchResponse<BankTransactionReportResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [Route("bank-transactions")]
    public async Task<IActionResult> BankTransactions([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _bankTransactionReport.Execute(query);

        return HandleResponse<EntitySearchResponse<BankTransactionReportResponse>>(response);
    }
    
    [HttpPost]
    [ProducesResponseType( typeof(EntitySearchResponse<CommerceTransactionReportResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [Route("commerce-transactions")]
    public async Task<IActionResult> CommerceTransactions([FromBody] OwnedDataSearchModel query)
    {
        IResponse response = await _commerceTransactionReport.Execute(query);

        return HandleResponse<EntitySearchResponse<CommerceTransactionReportResponse>>(response);
    }
}