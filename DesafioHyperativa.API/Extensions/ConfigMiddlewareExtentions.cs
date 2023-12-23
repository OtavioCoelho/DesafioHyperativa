using DesafioHyperativa.API.Middleware;

namespace DesafioHyperativa.API.Extensions;

public static class ConfigMiddlewareExtentions
{
    public static IApplicationBuilder ConfigMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<LoggingMiddleware>();
        return app;
    }
}
