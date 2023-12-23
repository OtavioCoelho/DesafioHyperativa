using Serilog;

namespace DesafioHyperativa.API.Extensions;

public static class ConfigSerilogExtension
{
    public static IServiceCollection AddSeriLog(this IServiceCollection services, WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();

        builder.Host.UseSerilog(
            (hostBuilderContext, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
            }
        );

        return services;
    }

    public static IApplicationBuilder ConfigSerilog(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}
