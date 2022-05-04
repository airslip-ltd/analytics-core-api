using Airslip.Analytics.Api.Controllers;
using Airslip.Analytics.Api.Docs.Core;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Logic;
using Airslip.Analytics.Reports;
using Airslip.Analytics.Services.SqlServer;
using Airslip.Common.Auth.AspNetCore.Extensions;
using Airslip.Common.Auth.AspNetCore.Middleware;
using Airslip.Common.Metrics;
using Airslip.Common.Middleware;
using Airslip.Common.Monitoring;
using Airslip.Common.Repository.Extensions;
using Airslip.Common.Security.Configuration;
using Airslip.Common.Services.AutoMapper.Extensions;
using Airslip.Common.Services.SqlServer;
using Airslip.Common.Services.SqlServer.Interfaces;
using Airslip.Common.Types;
using Airslip.Common.Types.Configuration;
using Airslip.Common.Utilities.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Http.Routing;

[assembly: InternalsVisibleTo("Airslip.Analytics.Api.Tests")]

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration)
    .Enrich.WithCorrelationIdHeader(ApiConstants.CorrelationIdName));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddCors()
    .AddHttpContextAccessor()
    .AddMvc();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(s =>
    {
        s.SerializerSettings.Converters = new List<JsonConverter>
        {
            new StringEnumConverter(new DefaultNamingStrategy())
        };
    });
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services
    .AddSwaggerGen(options =>
    {
        
        options.DocumentFilter<BasePathDocumentFilter>();
        options.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
        options.SchemaFilter<SwaggerExcludeSchemaFilter>();

        options.ExampleFilters();
        
        options.SwaggerDoc("2021.11",
            new OpenApiInfo
            {
                Title = "Analytics API",
                Version = "2021.11",
                Description = "Includes all API endpoints for data analytics." // Need to be more descriptive
            }
        );
        
        options.SwaggerDoc("2022.5",
            new OpenApiInfo
            {
                Title = "Analytics API",
                Version = "2022.5",
                Description = "Includes all API endpoints for data analytics." // Need to be more descriptive
            }
        );
        
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"e.g Bearer Api_Key.

Requests to the Airslip API are authenticated using the applications `Api_Key`. You can view and manage your credentials in the Airslip Dashboard.

An Api_Key provides connectivity to all authenticated Airslip API endpoints, so it is important to keep these credentials secure. Do not share your Api_Key in publicly accessible areas such as GitHub, client-side code, or easily accessible configuration settings.

Authentication is performed using Bearer Authentication. Your Api_Key should be sent as the token.

All requests should be made via HTTPS.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "Bearer",
                    Name = "Bearer",
                    In = ParameterLocation.Header,

                },
                new List<string>()
            }
        });

        options.CustomOperationIds(e => 
            e.ActionDescriptor.AttributeRouteInfo?.Name ?? e.ActionDescriptor.RouteValues["action"]?.ToSpacedPascalCase());
        options.SupportNonNullableReferenceTypes();
        string apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        string apiFilePath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
        options.IncludeXmlComments(apiFilePath, true);
        string coreXmlFile = $"{Assembly.GetAssembly(typeof(DashboardSnapshotModel))?.GetName().Name}.xml";
        string coreFilePath = Path.Combine(AppContext.BaseDirectory, coreXmlFile);
        options.IncludeXmlComments(coreFilePath);
    });

builder.Services
    .AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
    })
    .AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "VVV";
        options.SubstituteApiVersionInUrl = true;
    })
    .AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

// Add Options
builder.Services
    .AddOptions()
    .Configure<EnvironmentSettings>(builder.Configuration.GetSection(nameof(EnvironmentSettings)))
    .Configure<PublicApiSettings>(builder.Configuration.GetSection(nameof(PublicApiSettings)))
    .Configure<EventHubSettings>(builder.Configuration.GetSection(nameof(EventHubSettings)))
    .Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)))
    .Configure<EncryptionSettings>(builder.Configuration.GetSection(nameof(EncryptionSettings)))
    .Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services
    .AddAirslipJwtAuth(builder.Configuration);

builder.Services
    .AddHttpClient();

builder.Services
    .AddAutoMapper(mce => { mce.AddReportingMappings(); });

builder.Services
    .AddMetrics(builder.Configuration)
    .AddRepositories(builder.Configuration)
    .AddEntitySearch()
    .AddReportingServices()
    .AddAirslipSqlServer<SqlServerContext>(builder.Configuration);

builder
    .Services
    .AddApiLogicServices();

// If we want to add validation rules to API docs
// builder
//     .Services
//     .AddFluentValidationRulesToSwagger();

builder
    .Services
    .UseMonitoring();

ServiceDescriptor? type = builder.Services.FirstOrDefault(o => o.ServiceType == typeof(IQueryBuilder));
if(type is not null)
    builder.Services.Remove(type);
builder.Services.AddScoped<IQueryBuilder, QueryBuilderTEMP>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/2021.11/swagger.json", "Airslip.Analytics.Api 2021.11");
    c.SwaggerEndpoint("/swagger/2022.5/swagger.json", "Airslip.Analytics.Api 2022.5");
    c.RoutePrefix = string.Empty;
});

app
    .UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseMiddleware<ErrorHandlingMiddleware>()
    .UseMiddleware<JwtTokenMiddleware>()
    .UseCors(policy => policy
        .WithOrigins(builder.Configuration["AllowedHosts"].Split(";"))
        .WithExposedHeaders("Content-Disposition")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials())
    .UseEndpoints(endpoints =>
    {
        endpoints
            .MapControllers()
            .RequireAuthorization();
    });

app.Run();