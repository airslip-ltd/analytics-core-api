using Airslip.Analytics.Api.Docs.Examples.Poc;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Auth.AspNetCore.Implementations;
using Airslip.Common.Auth.AspNetCore.Schemes;
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

namespace Airslip.Analytics.Api.Controllers.Poc.Payroll;

/// <summary>
/// A collection of APIs that allows you to retrieve payroll for employees.
/// </summary>
[ApiController]
[ApiVersion("2022.5")]
[Consumes(Json.MediaType)]
[Produces(Json.MediaType)]
[Route("{version:apiVersion}/payroll/{businessId}/employees")]
[Authorize(AuthenticationSchemes = ApiKeyAuthenticationSchemeOptions.ApiKeyScheme)]
public class EmployeesController : ApiControllerBase
{
    public EmployeesController(
        ITokenDecodeService<UserToken> tokenDecodeService,
        IOptions<PublicApiSettings> publicApiOptions,
        ILogger logger) : base(tokenDecodeService,
        publicApiOptions, logger)
    {
    }

    /// <summary>
    /// Allows you to search for employees.
    /// </summary>
    /// <param name="businessId">The connected business identifier.</param>
    /// <param name="query">The search model for employees. You can use this to sort or search for any column within the model.</param>
    [HttpPost("search")]
    [ProducesResponseType(typeof(EntitySearchResponse<EmployeeModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetEmployees([FromRoute] string? businessId, [FromBody] QueryModel query)
    {
        EmployeeSearchModelExample example = new();

        IResponse response = example.GetExamples();

        return HandleResponse<EntitySearchResponse<EmployeeModel>>(response);
    }
    
    /// <summary>
    /// Use this method to get an employee.
    /// </summary>
    /// <param name="businessId">The connected business identifier.</param>
    /// <param name="id">The id of the employee.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetEmployee([FromRoute] string? businessId, [FromRoute] string id)
    {
        EmployeeSearchModelExample example = new();
        
        IResponse response = example.GetExamples();
        
        return HandleResponse<EmployeeModel>(response);
    }

    /// <summary>
    /// Use this method to create an employee.
    /// </summary>
    /// <param name="businessId">The connected business identifier.</param>
    /// <param name="body">The body of the employee to create.</param>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult CreateEmployee([FromRoute] string? businessId, [FromBody] EmployeeModel body)
    {
        return HandleResponse<CreatedModel>(body);
    }
    
    /// <summary>
    /// Use this method to update an employee.
    /// </summary>
    /// <param name="businessId">The connected business identifier.</param>
    /// <param name="id">The id of the employee to update.</param>
    /// <param name="body">The body of the employee to update.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult UpdateEmployee([FromRoute] string? businessId, [FromRoute] string id, [FromBody] EmployeeModel body)
    {
        return HandleResponse<Success>(body);
    }
}