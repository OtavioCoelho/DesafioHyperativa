using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using DesafioHyperativa.Service.Services.Base;

namespace DesafioHyperativa.Service.Services;
public class UserService : Service<User>, IUserService
{
    private readonly IUserRepository _repositoryUser;
    public UserService(
        IUserRepository repository,
        IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
        _repositoryUser = repository;
    }

    public async Task<User> GetByLoginAsync(User user)
    {
        await ValidateUserGetByLogin(user);
        return await _repositoryUser.GetByLoginAsync(user);
    }
    public async Task ValidateUserGetByLogin(User user)
    {
        if (user is null)
            throw new BusinessException("User not found.");
        if (string.IsNullOrEmpty(user.Email))
            throw new BusinessException("Email not informed.");
        if (string.IsNullOrEmpty(user.Password))
            throw new BusinessException("Password not informed.");
        
        await Task.CompletedTask;
    }
}
