using DesafioHyperativa.Domain.EnumTypes;
using DesafioHyperativa.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioHyperativa.Repository.Extensions;
public static class ConfigDatabase
{
    public static IServiceCollection AddHyperativaDbContext(this IServiceCollection services, IConfiguration configuration, bool enableSensitiveDataLogging)
    {
        DatabaseProviderEnum databaseProviderEnum = Enum.Parse<DatabaseProviderEnum>(configuration.GetSection("DatabaseProvider")["Provider"]);

        services.AddDbContext<HyperativaDbContext>(options =>
        {
            switch (databaseProviderEnum)
            {
                case DatabaseProviderEnum.Sqlite:
                    options.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
                    break;
            }
            options.EnableSensitiveDataLogging(enableSensitiveDataLogging);
        });

        return services;
    }
}
