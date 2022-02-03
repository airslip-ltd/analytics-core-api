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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airslip.Analytics.Api.Controllers
{
    [ApiController]    
    [ApiVersion("1.0")]
    [Produces(Json.MediaType)]
    [Route("v{version:apiVersion}/snapshot")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SnapshotController : ApiControllerBase
    {
        private readonly IDashboardSnapshotService _dashboardSnapshotService;
        private readonly IRevenueAndRefundsService _revenueAndRefundsService;

        public SnapshotController(IDashboardSnapshotService dashboardSnapshotService,
            IRevenueAndRefundsService revenueAndRefundsService,
            ITokenDecodeService<UserToken> tokenDecodeService, 
            IOptions<PublicApiSettings> publicApiOptions, ILogger logger) 
            : base(tokenDecodeService, publicApiOptions, logger)
        {
            _dashboardSnapshotService = dashboardSnapshotService;
            _revenueAndRefundsService = revenueAndRefundsService;
        }
        
        [HttpGet]
        [Route("{snapshotType}")]
        [ProducesResponseType(typeof(DashboardSnapshotModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSnapshot([FromRoute]DashboardSnapshotType snapshotType,
            [FromQuery]int dayRange = 7,
            [FromQuery]int statRange = 10)
        {
            IResponse response = await _dashboardSnapshotService
                .GetSnapshotFor(snapshotType, dayRange, statRange);
            
            return HandleResponse<DashboardSnapshotModel>(response);
        }
        
        [HttpGet]
        [Route("revenue")]
        [ProducesResponseType(typeof(RevenueAndRefundsByYearModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRevenue(
            [FromQuery]int year = 7)
        {
            IResponse response = await _revenueAndRefundsService
                .GetRevenueAndRefunds(year);
            
            return HandleResponse<RevenueAndRefundsByYearModel>(response);
        }
    }
}