using DesafioHyperativa.Domain.Entities.Base;

namespace DesafioHyperativa.Domain.Contracts.Repositories;
public interface IRepository<TEntity> where TEntity : Entity, new()
{
    Task<TEntity> GetAsync(Guid id);
    Task<TEntity> SaveAsync(TEntity entity);
    Task<IEnumerable<TEntity>> SaveAsync(IEnumerable<TEntity> listEntity);
}
