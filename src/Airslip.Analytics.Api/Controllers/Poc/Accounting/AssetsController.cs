using Airslip.Analytics.Api.Docs.Examples.Poc;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Auth.AspNetCore.Implementations;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types;
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

namespace Airslip.Analytics.Api.Controllers.Poc.Accounting;

/// <summary>
/// The Assets API exposes fixed asset related functions and can be used for a variety of purposes such as creating assets, retrieving asset valuations and visualising asset depreciation.
/// </summary>
[ApiController]
[ApiVersion("2022.5")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("{version:apiVersion}/assets/{businessId}")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AssetsController : ApiControllerBase
{
    public AssetsController(
        ITokenDecodeService<UserToken> tokenDecodeService,
        IOptions<PublicApiSettings> publicApiOptions,
        ILogger logger) : base(tokenDecodeService,
        publicApiOptions, logger)
    {
    }

    /// <summary>
    /// Allows you to retrieve assets
    /// </summary>
    /// <param name="businessId">The connected business identifier</param>
    /// <param name="query">The search model for assets. You can use this to sort or search for any column within the model</param>
    [HttpPost("search")]
    [ProducesResponseType(typeof(EntitySearchResponse<AssetModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetAssets([FromRoute] string? businessId, [FromBody] QueryModel query)
    {
        AssetSearchModelExample example = new();

        IResponse response = example.GetExamples();

        return HandleResponse<EntitySearchResponse<AssetModel>>(response);
    }
    
    /// <summary>
    /// Use this method to get an asset
    /// </summary>
    /// <param name="businessId">The connected business identifier</param>
    /// <param name="id">The id of the asset</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AssetModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetAsset([FromRoute] string? businessId, [FromRoute] string id)
    {
        AssetSearchModelExample example = new();

        IResponse response = example.GetExamples();
        
        return HandleResponse<AssetModel>(response);
    }

    /// <summary>
    /// Use this method to create draft fixed assets.
    /// </summary>
    /// <param name="businessId">The connected business identifier</param>
    /// <param name="body">The body of the asset to create</param>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult CreateAsset([FromRoute] string? businessId, [FromBody] AssetModel body)
    {
        return HandleResponse<CreatedModel>(body);
    }
    
    /// <summary>
    /// Use this method to update assets.   
    /// </summary>
    /// <param name="businessId">The connected business identifier</param>
    /// <param name="id">The id of the asset to update</param>
    /// <param name="body">The body of the asset to update</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult UpdateAsset([FromRoute] string? businessId, [FromRoute] string id, [FromBody] AssetModel body)
    {
        return HandleResponse<Success>(body);
    }
}