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
    [Route("v{version:apiVersion}/balance")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountsController : ApiControllerBase
    {
        private readonly IBalanceService _balanceService;

        public AccountsController(IBalanceService balanceService, ITokenDecodeService<UserToken> tokenDecodeService, 
            IOptions<PublicApiSettings> publicApiOptions, ILogger logger) 
            : base(tokenDecodeService, publicApiOptions, logger)
        {
            _balanceService = balanceService;
        }
        
        [HttpGet]
        [Route("summary")]
        [ProducesResponseType(typeof(AccountBalanceSummaryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccounts()
        {
            IResponse response = await _balanceService
                .GetAccountBalances();
            
            return HandleResponse<AccountBalanceSummaryResponse>(response);
        }
        
        // Balance by account
        // Metric by account
        
    }
}