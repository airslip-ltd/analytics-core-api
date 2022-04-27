using Airslip.Analytics.Api.Docs.Examples.Poc;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Poc;
using Airslip.Common.Auth.AspNetCore.Implementations;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Models;
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

namespace Airslip.Analytics.Api.Controllers.Poc;

/// <summary>
/// A description for a group of APIs
/// </summary>
[ApiController]    
[ApiVersion("1.0")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("v{version:apiVersion}/bank-transactions")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BankTransactionsController : ApiControllerBase
{
    public BankTransactionsController(
        ITokenDecodeService<UserToken> tokenDecodeService, 
        IOptions<PublicApiSettings> publicApiOptions, ILogger logger) 
        : base(tokenDecodeService, publicApiOptions, logger)
    {
    }
    
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    /// <param name="query">A parameter description should go here</param>
    [HttpPost]
    [Route("search")]
    [ProducesResponseType(typeof(EntitySearchResponse<BankTransactionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetBankTransactions([FromBody] OwnedDataSearchModel query)
    {
        BankTransactionsResponseExample example = new();
        
        IResponse response = example.GetExamples();
            
        return HandleResponse<BankTransactionsResponse>(response);
    }
    
    /// <summary>
    /// A description about a specific API should go here
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BankTransactionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetBankTransaction([FromRoute] string id)
    {
        BankTransactionResponseExample example = new();
        
        IResponse response = example.GetExamples();
            
        return HandleResponse<BankTransactionResponse>(response);
    }
}