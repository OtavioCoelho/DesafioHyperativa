using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Contracts.Services.Base;
using DesafioHyperativa.Service.Services;
using DesafioHyperativa.Service.Services.Base;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioHyperativa.Service.Extensions;
public static class ServiceInjectionRegisterExtension
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped(typeof(IService<>), typeof(Service<>));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILotService, LotService>();
        services.AddScoped<ICardService, CardService>();
        return services;
    }
}
