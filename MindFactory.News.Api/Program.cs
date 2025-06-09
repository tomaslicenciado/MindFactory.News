// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MindFactory.News.Api.Configuration;
using MindFactory.News.Api.Configuration.SwaggerConfiguration;
using MindFactory.News.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSolucion();

var basepath = builder.Environment.EnvironmentName.Equals("AZ-DEV") ? "/ticket" : string.Empty;

builder.Services.AddSwaggerGen(c =>
{
    if (!string.IsNullOrEmpty(basepath))
    {
        c.DocumentFilter<SwaggerDocumentFilter>();
    }
});

var app = builder.Build();

app.UseSwagger();

var provider = app.Services.GetService<IApiVersionDescriptionProvider>()!;
app.UseSwaggerUI(
    options =>
    {
        // build a swagger endpoint for each discovered API version
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"{basepath}/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });

app.UseHttpsRedirection();

app.UseRouting();

// autenticacion
app.UseAuthentication();
app.UseAuthorization();

if (builder.Configuration.IsCORSEnabled())
{
    app.UseCors();
}

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapHealthChecks("/health", new HealthCheckOptions()
    {
        // This custom writer formats the detailed status as JSON.
        // ResponseWriter = Sgrtch.Gser.Tickets.Api.Health.HealthResponsesWriter.WriteResponses
    });
    _ = endpoints.MapControllers();
});

app.Run();
