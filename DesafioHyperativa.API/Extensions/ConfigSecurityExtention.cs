using DesafioHyperativa.Domain.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DesafioHyperativa.API.Extensions;

public static class ConfigSecurityExtention
{
    public static IServiceCollection ConfigSecurity(this IServiceCollection services)
    {
        services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        return services;
    }

    public static IApplicationBuilder ConfigSecurity(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }

}
