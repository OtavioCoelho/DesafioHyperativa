using DesafioHyperativa.Domain.Entities;

namespace DesafioHyperativa.Domain.Contracts.Repositories;
public interface IUserRepository : IRepository<User>
{
    Task<User> GetByLoginAsync(User user);
}
