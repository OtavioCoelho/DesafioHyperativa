using DesafioHyperativa.API.Filters;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DesafioHyperativa.API.Extensions;

public static class ConfigSwaggerExtension
{
    public static IServiceCollection ConfigSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API Desafio Hyperativa",
                Description = "The API is designed to register and query a credit card or a batch of credit cards. Additionally, it allows querying based on the card number.",
                Contact = new OpenApiContact
                {
                    Name = "Otávio A. F. Coelho",
                    Email = "otavioafc@gmail.com",
                    Url = new Uri("https://github.com/OtavioCoelho/DesafioHyperativa")
                }
            }); ;
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.SchemaFilter<SwaggerIgnoreFilter>();
            c.OperationFilter<IgnorePropertyFilter>();

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static IApplicationBuilder ConfigUISwagger(this IApplicationBuilder app)
    {
        app.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
        });

        app.UseSwaggerUI(c =>
        {
            string currentVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            c.SwaggerEndpoint(string.Format("/swagger/v1/swagger.json", currentVersion),
                    string.Format("API v{0}", currentVersion));
        });

        return app;
    }

}
