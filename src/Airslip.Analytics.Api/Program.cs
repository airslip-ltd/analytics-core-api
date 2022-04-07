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
using Airslip.Common.Types;
using Airslip.Common.Types.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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

builder.Services
    .AddSwaggerGen(options =>
    {
        string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        string filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(filePath);
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

        options.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Analytics API",
                Version = "1.0.0",
                Description = "Includes all API endpoints for data analytics."
            }
        );
    });

builder.Services
    .AddApiVersioning(options => { options.ReportApiVersions = true; })
    .AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

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
    .AddAutoMapper(mce =>
    {
        mce.AddReportingMappings();
    });

builder.Services
    .AddMetrics(builder.Configuration)
    .AddRepositories(builder.Configuration)
    .AddEntitySearch()
    .AddReportingServices()
    .AddAirslipSqlServer<SqlServerContext>(builder.Configuration);

builder
    .Services
    .AddApiLogicServices();

builder.Services
    .UseHealthChecks();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Airslip.CustomerPortal.Api v1");
    c.RoutePrefix = string.Empty;
});

app
    .UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseMiddleware<ErrorHandlingMiddleware>()
    .UseMiddleware<JwtTokenMiddleware>()
    .UseCors(policy  => policy
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