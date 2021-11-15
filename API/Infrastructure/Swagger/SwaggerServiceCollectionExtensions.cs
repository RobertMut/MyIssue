using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MyIssue.Main.API.Infrastructure.Swagger.Filters;
using MyIssue.Main.API.Infrastructure.Swagger.Options;

namespace MyIssue.Main.API.Infrastructure.Swagger
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerOptions = SwaggerOptions.ReadFromIConfiguration(configuration);
            services.AddTransient<IOptions<SwaggerOptions>>(provider => new OptionsWrapper<SwaggerOptions>(swaggerOptions));

            var authority = configuration["AuthServiceHttpExternalUrl"];
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", swaggerOptions.ApiInfo);
                options.CustomSchemaIds(x => x.FullName);

                string xmlPath = null;

                if (swaggerOptions.XmlCommentsFilePath != null)
                    xmlPath = swaggerOptions.XmlCommentsFilePath;
                else
                {
                    var autoPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml");
                    if (File.Exists(autoPath))
                        xmlPath = autoPath;
                }

                if (xmlPath != null)
                    options.IncludeXmlComments(xmlPath);

                if (swaggerOptions.OAuth != null)
                {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows()
                        {
                            Implicit = new OpenApiOAuthFlow()
                            {
                                AuthorizationUrl = new Uri(Path.Combine(authority + "/connect/authorize")),
                                TokenUrl = new Uri(Path.Combine(authority + "/connect/token")),
                                Scopes = swaggerOptions.OAuth.Scopes
                            }
                        }
                    });
                    options.OperationFilter<AuthorizeCheckOperationFilter>();
                }
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}