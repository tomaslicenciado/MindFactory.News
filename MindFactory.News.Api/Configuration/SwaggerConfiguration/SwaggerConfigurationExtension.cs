// <copyright file="SwaggerConfigurationExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Api.Configuration.SwaggerConfiguration
{
    using System.Reflection;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.PlatformAbstractions;
    using Microsoft.OpenApi.Models;
    using MindFactory.News.Api.Extensions;
    using Swashbuckle.AspNetCore.Filters;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public static class SwaggerConfigurationExtension
    {
        public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(option =>
            {
                option.ReportApiVersions = true;

                // option.ApiVersionReader = new HeaderApiVersionReader("api-version");
                // option.AssumeDefaultVersionWhenUnspecified = true;
                // option.DefaultApiVersion = new ApiVersion(1, 0);
            });

            builder.Services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            builder.Services.AddSwaggerGen(options =>
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(SwaggerDefaultValues).GetTypeInfo().Assembly.GetName().Name + ".xml";
                var commentsXmlFilePath = Path.Combine(basePath, fileName);

                options.CustomSchemaIds(x => x.FullName);
                options.OperationFilter<SwaggerDefaultValues>();
                options.IncludeXmlComments(commentsXmlFilePath);

                if (builder.Configuration.IsSecurityEnabled())
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Ingrese Token: bearer + [espacio] + token obtenido ",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme,
                                },
                            },
                            new List<string>()
                        },
                    });
                }

                options.ExampleFilters();
            });

            builder.Services.AddSwaggerExamplesFromAssemblyOf<SwaggerDefaultValues>();

            return builder;
        }
    }
}