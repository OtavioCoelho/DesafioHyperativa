using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace DesafioHyperativa.Repository.CrossCutting;
public class UnitOfWork : IUnitOfWork
{
    private readonly HyperativaDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    private bool disposedValue;
    private IDbContextTransaction _transaction;


    public UnitOfWork(HyperativaDbContext context,
        ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
            return;

        _transaction = await _context.Database.BeginTransactionAsync();
        await _context.Database.UseTransactionAsync(_transaction.GetDbTransaction());
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();

        _transaction = null;
    }

    public async Task RollBackAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public bool IsInTransaction() => _context.Database.CurrentTransaction != null;
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }

                if (_context != null)
                    _context.Dispose();
            }
            disposedValue = true;
        }
    }
}
