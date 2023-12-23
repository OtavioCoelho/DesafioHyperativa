using DesafioHyperativa.Domain.Entities.Base;

namespace DesafioHyperativa.Domain.Contracts.Services.Base;
public interface IService<TEntity> where TEntity : Entity, new()
{
    Task<TEntity> GetAsync(Guid id);
    Task<TEntity> SaveAsync(TEntity entity);
    Task<IEnumerable<TEntity>> SaveAsync(IEnumerable<TEntity> listEntity);
}
