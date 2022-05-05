using Airslip.Common.Types.Configuration;
using Airslip.Common.Utilities.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Core;

public class BasePathDocumentFilter : IDocumentFilter
{
    private readonly PublicApiSettings _publicApiSettings;

    public BasePathDocumentFilter(IOptions<PublicApiSettings> publicApiOptions)
    {
        _publicApiSettings = publicApiOptions.Value;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        string settingName = context.DocumentName == "2021.11" ? "Base" : "ExternalApi";
        
        string baseUri = _publicApiSettings.GetSettingByName(settingName).ToBaseUri();
        
        swaggerDoc.Servers = new List<OpenApiServer>
        {
            new() { Url = baseUri },
        };
    }
}