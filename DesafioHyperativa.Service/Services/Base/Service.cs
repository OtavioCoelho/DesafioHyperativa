using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Contracts.Services.Base;
using DesafioHyperativa.Domain.Entities.Base;

namespace DesafioHyperativa.Service.Services.Base;
public class Service<TEntity> : IService<TEntity> where TEntity : Entity, new()
{
    private readonly IRepository<TEntity> _repository;
    private readonly IUnitOfWork _uow;
    public Service(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _uow = unitOfWork;
    }

    public virtual async Task<TEntity> GetAsync(Guid id)
        => await _repository.GetAsync(id);


    public virtual async Task<TEntity> SaveAsync(TEntity entity)
    {
        try
        {
            await _uow.BeginTransactionAsync();
            await _repository.SaveAsync(entity);
            await _uow.CommitAsync();
            return entity;
        }
        catch (Exception)
        {
            await _uow.RollBackAsync();
            throw;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> SaveAsync(IEnumerable<TEntity> listEntity)
    {
        try
        {
            await _uow.BeginTransactionAsync();
            await _repository.SaveAsync(listEntity);
            await _uow.CommitAsync();
            return listEntity;
        }
        catch (Exception)
        {
            await _uow.RollBackAsync();
            throw;
        }
    }
}
