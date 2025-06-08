using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindFactory.News.Api.Configuration.SwaggerConfiguration;
using MindFactory.News.Api.Extensions;
using MindFactory.News.Application.Authors.Commands.AddAuthor;
using MindFactory.News.Application.Interfaces;
using MindFactory.News.Infraestructure.Persistence;

namespace MindFactory.News.Api.Configuration
{
    public static class SolutionConfiguration
    {
        public static void ConfigureSolucion(this WebApplicationBuilder builer)
        {
            builer.ConfigureSetting()
                .ConfigureLogger()
                .ConfigureApplication()
                .ConfigureSwagger()
                .ConfigureServices()
                .ConfigureHealth()
                .ConfigureCORS()
                .ConfigureSecurity();

        }

        private static WebApplicationBuilder ConfigureSetting(this WebApplicationBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder();
            if (builder.Environment.IsProduction())
            {
                configurationBuilder
                    .SetBasePath(builder.Environment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            }
            else
            {
                configurationBuilder
                    .SetBasePath(builder.Environment.ContentRootPath)
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false,
                        reloadOnChange: true);
            }

            _ = configurationBuilder.Build();
            configurationBuilder.AddEnvironmentVariables();

            return builder;
        }

        private static WebApplicationBuilder ConfigureLogger(this WebApplicationBuilder builder)
        {
            return builder;
        }

        private static WebApplicationBuilder ConfigureApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow;
                });
            return builder;
        }

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies([
                    typeof(Program).Assembly, typeof(AddAuthorCommandHandler).Assembly
                ]);
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "admin");
                }));

            builder.Services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());
            

            return builder;
        }

        private static WebApplicationBuilder ConfigureHealth(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                .AddSqlServer(builder.Configuration.GetConnectionString("Postgres")!, name: "db-healthCheck");

            return builder;
        }

        private static WebApplicationBuilder ConfigureCORS(this WebApplicationBuilder builder)
        {
            var corsValue = builder.Configuration.IsCORSEnabled();
            if (corsValue)
            {
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(pol =>
                    {
                        pol.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                });
            }

            return builder;
        }

        public static WebApplicationBuilder ConfigureSecurity(this WebApplicationBuilder builder)
        {
            return builder;
        }
    }
}