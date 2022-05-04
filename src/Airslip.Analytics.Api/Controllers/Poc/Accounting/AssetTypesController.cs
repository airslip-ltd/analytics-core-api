using Airslip.Analytics.Api.Docs.Examples.Poc;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Models.Poc;
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

namespace Airslip.Analytics.Api.Controllers.Poc.Accounting;

/// <summary>
/// The Assets API exposes fixed asset related functions and can be used for a variety of purposes such as creating assets, retrieving asset valuations and visualising asset depreciation.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Assets")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("v{version:apiVersion}/asset-types")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AssetTypesController : ApiControllerBase
{
    public AssetTypesController(
        ITokenDecodeService<UserToken> tokenDecodeService,
        IOptions<PublicApiSettings> publicApiOptions,
        ILogger logger) : base(tokenDecodeService,
        publicApiOptions, logger)
    {
    }

    /// <summary>
    /// Allows you to retrieve asset types
    /// </summary>
    /// <param name="query">The search model for asset types</param>
    [HttpPost("search")]
    [ProducesResponseType(typeof(EntitySearchResponse<AssetTypeModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetAssetTypes([FromBody] OwnedDataSearchModel query)
    {
        AssetTypeSearchModelExample example = new();

        IResponse response = example.GetExamples();

        return HandleResponse<EntitySearchResponse<AssetTypeModel>>(response);
    }
    
    /// <summary>
    /// Use this method to get an asset type
    /// </summary>
    /// <param name="id">The id of the employee</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AssetTypeModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetAssetType([FromRoute] string id)
    {
        AssetTypeSearchModelExample example = new();
        
        IResponse response = example.GetExamples();
        
        return HandleResponse<AssetTypeModel>(response);
    }

    /// <summary>
    /// Use this method to create asset types
    /// </summary>
    /// <param name="body">The body of the asset type to create</param>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult CreateAssetType([FromBody] AssetModel body)
    {
        return HandleResponse<CreatedModel>(body);
    }
    
    /// <summary>
    /// Use this method to update assets
    /// </summary>
    /// <param name="id">The id of the asset type to update</param>
    /// <param name="body">The body of the asset type to update</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CreatedModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult UpdateAssetType([FromRoute] string id, [FromBody] AssetModel body)
    {
        return HandleResponse<CreatedModel>(body);
    }
}