using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Core;

public class RequireNonNullablePropertiesSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null) 
            return;
        
        foreach (KeyValuePair<string, OpenApiSchema> prop in schema.Properties)
        {
            if (!prop.Value.Nullable)
                schema.Required.Add(prop.Key);
        }
    }
}
