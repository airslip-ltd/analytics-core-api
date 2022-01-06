using System;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace Airslip.Bootstrap.Function.Configuration
{
    public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiInfo Info { get; set; } = new()
        {
            Version = "v1",
            Title = "QR Code Matching API",
            Description = "This is the API used for requesting QR Code matching.",
            Contact = new OpenApiContact()
            {
                Name = "Airslip Support",
                Email = "support@airslip.com",
                Url = new Uri("https://www.airslip.com"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            }
        };

        public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
    }
}