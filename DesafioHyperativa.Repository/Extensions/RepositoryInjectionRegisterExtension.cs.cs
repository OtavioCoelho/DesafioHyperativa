using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Repository.Repositories;
using DesafioHyperativa.Repository.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioHyperativa.Repository.Extensions;
public static class RepositoryInjectionRegisterExtension
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ILotRepository, LotRepository>();

        return services;
    }
}
