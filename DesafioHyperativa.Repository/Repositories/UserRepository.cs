using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Repository.Contexts;
using DesafioHyperativa.Repository.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DesafioHyperativa.Repository.Repositories;
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(
        HyperativaDbContext context,
        IUnitOfWork unitOfWork,
        ILogger<Repository<User>> logger) : base(context, unitOfWork, logger)
    {

    }

    public async Task<User> GetByLoginAsync(User user)
    {
        return await this._dbSet
            .SingleOrDefaultAsync(x => x.Email == user.Email
                                    && x.Password == user.Password);
    }
}
