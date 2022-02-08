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
    [Route("v{version:apiVersion}/transactions")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TransactionController : ApiControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService, ITokenDecodeService<UserToken> tokenDecodeService, 
            IOptions<PublicApiSettings> publicApiOptions, ILogger logger) 
            : base(tokenDecodeService, publicApiOptions, logger)
        {
            _transactionService = transactionService;
        }
        
        [HttpGet]
        [Route("commerce/accounts")]
        [ProducesResponseType(typeof(SimpleListResponse<MerchantAccountSummaryModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCommerceAccounts()
        {
            IResponse response = await _transactionService
                .GetMerchantAccounts();
            
            return HandleResponse<SimpleListResponse<MerchantAccountSummaryModel>>(response);
        }
        
        [HttpGet]
        [Route("banking/recent")]
        [ProducesResponseType(typeof(SimpleListResponse<TransactionSummaryModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBankingRecent(
            [FromQuery]int limit = 10,
            [FromQuery]string? accountId = null
            )
        {
            IResponse response = await _transactionService
                .GetBankingTransactions(limit, accountId);
            
            return HandleResponse<SimpleListResponse<TransactionSummaryModel>>(response);
        }
        
        [HttpGet]
        [Route("commerce/recent")]
        [ProducesResponseType(typeof(SimpleListResponse<TransactionSummaryModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMerchantRecent(
            [FromQuery]int limit = 10,
            [FromQuery]string? accountId = null
        )
        {
            IResponse response = await _transactionService
                .GetCommerceTransactions(limit, accountId);
            
            return HandleResponse<SimpleListResponse<TransactionSummaryModel>>(response);
        }
    }
}