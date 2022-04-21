using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
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
using System.Threading.Tasks;

namespace Airslip.Analytics.Api.Controllers;

/// <summary>
/// This does not work
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("v{version:apiVersion}/snapshot")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SnapshotController : ApiControllerBase
{
    private readonly IDashboardSnapshotService _dashboardSnapshotService;
    private readonly IRevenueAndRefundsService _revenueAndRefundsService;
    private readonly IDebitsAndCreditsService _debitsAndCreditsService;

    public SnapshotController(
        IDashboardSnapshotService dashboardSnapshotService,
        IRevenueAndRefundsService revenueAndRefundsService,
        IDebitsAndCreditsService debitsAndCreditsService,
        ITokenDecodeService<UserToken> tokenDecodeService, 
        IOptions<PublicApiSettings> publicApiOptions, ILogger logger) 
        : base(tokenDecodeService, publicApiOptions, logger)
    {
        _dashboardSnapshotService = dashboardSnapshotService;
        _revenueAndRefundsService = revenueAndRefundsService;
        _debitsAndCreditsService = debitsAndCreditsService;
    }
        
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    [HttpPost]
    [Route("{snapshotType}")]
    [ProducesResponseType(typeof(DashboardSnapshotModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSnapshot([FromRoute]DashboardSnapshotType snapshotType,
        [FromBody] OwnedSnapshotSearchModel query,
        [FromQuery]int dayRange = 7,
        [FromQuery]int statRange = 10,
        [FromQuery]string? integrationId = null)
    {
        IResponse response = await _dashboardSnapshotService
            .GetSnapshotFor(query, snapshotType, dayRange, statRange, integrationId);
            
        return HandleResponse<DashboardSnapshotModel>(response);
    }
        
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    [HttpPost]
    [Route("revenue")]
    [ProducesResponseType(typeof(DashboardGraphSeriesModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRevenue(
        [FromBody] OwnedSeriesSearchModel query)
    {
        IResponse response = await _revenueAndRefundsService
            .Execute(query);
            
        return HandleResponse<DashboardGraphSeriesModel>(response);
    }
        
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    [HttpPost]
    [Route("cashflow")]
    [ProducesResponseType(typeof(DashboardGraphSeriesModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCashflow(
        [FromBody] OwnedSeriesSearchModel query)
    {
        IResponse response = await _debitsAndCreditsService
            .Execute(query);
            
        return HandleResponse<DashboardGraphSeriesModel>(response);
    }
}