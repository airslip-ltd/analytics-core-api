using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Utilities.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Airslip.Analytics.Api.Docs.Core;

public class SwaggerExcludeSchemaFilter : ISchemaFilter
{
    private static readonly IEnumerable<string> _defaultExcludedProperties = new List<string>
    {
        nameof(EntityStatus).ToCamelCase()
    };

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null || context.Type == null)
            return;

        IEnumerable<PropertyInfo> excludedProperties =  context.Type.GetProperties()
            .Where(t => 
                t.GetCustomAttribute<SwaggerIgnoreAttribute>() 
                != null);
        
        foreach (PropertyInfo excludedProperty in excludedProperties)
        {
            if (schema.Properties.ContainsKey(excludedProperty.Name))
                schema.Properties.Remove(excludedProperty.Name);
        }
        
        foreach (string excludedProperty in _defaultExcludedProperties
                     .Where(excludedProperty => schema.Properties.ContainsKey(excludedProperty)))
        {
            schema.Properties.Remove(excludedProperty);
        }
    }
}