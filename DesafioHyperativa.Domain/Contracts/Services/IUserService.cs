using DesafioHyperativa.Domain.Contracts.Services.Base;
using DesafioHyperativa.Domain.Entities;

namespace DesafioHyperativa.Domain.Contracts.Services;
public interface IUserService : IService<User>
{
    Task<User> GetByLoginAsync(User user);
    Task ValidateUserGetByLogin(User user);
}
