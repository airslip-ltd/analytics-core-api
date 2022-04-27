using Airslip.Analytics.Core.Entities.Unmapped;
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
[Route("v{version:apiVersion}/data-lists")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DataController : ApiControllerBase
{
    private readonly IDataListService _dataListService;

    public DataController(IDataListService dataListService,
        ITokenDecodeService<UserToken> tokenDecodeService, 
        IOptions<PublicApiSettings> publicApiOptions, ILogger logger) 
        : base(tokenDecodeService, publicApiOptions, logger)
    {
        _dataListService = dataListService;
    }
        
    [HttpPost]
    [Route("currencies")]
    [ProducesResponseType(typeof(DataSearchResponse<CurrencySnapshot>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAccounts([FromBody] DataSearchModel query)
    {
        IResponse response = await _dataListService
            .GetCurrencies(query);
            
        return HandleResponse<DataSearchResponse<CurrencySnapshot>>(response);
    }
}