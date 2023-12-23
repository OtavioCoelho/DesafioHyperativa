using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Entities.Base;
using DesafioHyperativa.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DesafioHyperativa.Repository.Repositories.Base;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly HyperativaDbContext _context;
    protected readonly IUnitOfWork _uow;
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly ILogger<Repository<TEntity>> _logger;

    public Repository(HyperativaDbContext context,
        IUnitOfWork unitOfWork,
        ILogger<Repository<TEntity>> logger)
    {
        _uow = unitOfWork;
        _logger = logger;
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetAsync(Guid id)
        => await _dbSet.FindAsync(id);

    public virtual async Task<TEntity> SaveAsync(TEntity entity)
    {
        if (entity.Id != Guid.Empty)
        {
            _dbSet.Attach(entity);
            _dbSet.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
        }
        else
        {
            await _dbSet.AddAsync(entity);
        }
        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> SaveAsync(IEnumerable<TEntity> listEntity)
    {
        if (listEntity.Any(x => x.Id != Guid.Empty))
        {
            _dbSet.AttachRange(listEntity);
            foreach (var entity in listEntity)
                _dbSet.Entry(entity).State = EntityState.Modified;
            _dbSet.UpdateRange(listEntity);
        }
        else
        {
            await _dbSet.AddRangeAsync(listEntity);
        }
        return listEntity;
    }
}
