using Airslip.Common.Types.Configuration;
using Airslip.Common.Utilities.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Core;

public class BasePathDocumentFilter : IDocumentFilter
{
    private readonly string _baseUri;

    public BasePathDocumentFilter(IOptions<PublicApiSettings> publicApiOptions)
    {
        _baseUri = publicApiOptions.Value.GetSettingByName("ExternalApi").ToBaseUri();
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Servers = new List<OpenApiServer>
        {
            new() { Url = _baseUri },
        };
    }
}