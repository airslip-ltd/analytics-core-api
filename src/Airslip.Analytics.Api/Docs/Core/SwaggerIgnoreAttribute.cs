using System;

namespace Airslip.Analytics.Api.Docs.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class SwaggerIgnoreAttribute : Attribute
{
}