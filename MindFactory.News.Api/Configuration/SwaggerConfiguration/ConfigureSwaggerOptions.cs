using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MindFactory.News.Api.Configuration.SwaggerConfiguration
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;
        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in provider.ApiVersionDescriptions)
            {
                try
                {
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }
                catch (Exception)
                {
                }
            }
        }
        public static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "4Kings.POS.Api",
                Version = description.ApiVersion.ToString(),
                Description = "Api para la gestión de operaciones para POS"
            };
            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }
            return info;
        }
    }
}