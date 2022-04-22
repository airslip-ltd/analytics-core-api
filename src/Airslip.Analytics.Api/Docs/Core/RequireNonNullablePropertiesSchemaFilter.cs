using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Core;

public class RequireNonNullablePropertiesSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (model.Properties == null) 
            return;
        
        foreach (KeyValuePair<string, OpenApiSchema> prop in model.Properties)
        {
            if (!prop.Value.Nullable)
                model.Required.Add(prop.Key);
        }
    }
}
