using Airslip.Common.Monitoring.Interfaces;
using Airslip.Common.Monitoring.Models;
using Airslip.Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Airslip.Analytics.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces(Json.MediaType)]
    [Route("v{version:apiVersion}/heartbeat")]
    public class HeartbeatController : ControllerBase
    {
        private readonly IHealthCheckService _healthCheckService;

        public HeartbeatController(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(HealthCheckResponse), StatusCodes.Status200OK)]
        [Route("health")]
        public async Task<IActionResult> Health()
        {
            HealthCheckResponse heartbeatResponse = await _healthCheckService.CheckServices();
            
            return Ok(heartbeatResponse);
        }
    }
}