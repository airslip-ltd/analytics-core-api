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
        [ProducesResponseType(typeof(BankCurrentSnapshotModel), StatusCodes.Status200OK)]
        [ProducesResponseType( typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccounts()
        {
            
            // CurrentBalanceModel response = new()
            // {
            //     Balance = 100000,
            //     Movement = -0.5,
            //     TimeStamp = 1642163605000,
            //     Metrics = new List<BalanceMetric>()
            // };
            // Random rnd = new();
            // for (int i = 0; i < 10; i++)
            // {
            //     response.Metrics.Add(new BalanceMetric()
            //     {
            //         Balance = rnd.NextInt64(150000)
            //     });
            // }
            IResponse response = await _balanceService
                .GetCurrentBalance();
            
            
            
            return HandleResponse<BankCurrentSnapshotModel>(response);
        }
    }
}