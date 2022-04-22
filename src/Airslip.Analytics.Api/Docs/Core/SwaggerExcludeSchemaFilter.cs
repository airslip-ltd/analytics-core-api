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

    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (model.Properties == null || context.Type == null)
            return;

        IEnumerable<PropertyInfo> excludedProperties =  context.Type.GetProperties()
            .Where(t => 
                CustomAttributeExtensions.GetCustomAttribute<SwaggerIgnoreAttribute>((MemberInfo) t) 
                != null);
        
        foreach (PropertyInfo excludedProperty in excludedProperties)
        {
            if (model.Properties.ContainsKey(excludedProperty.Name))
                model.Properties.Remove(excludedProperty.Name);
        }
        
        foreach (string excludedProperty in _defaultExcludedProperties
                     .Where(excludedProperty => model.Properties.ContainsKey(excludedProperty)))
        {
            model.Properties.Remove(excludedProperty);
        }
    }
}