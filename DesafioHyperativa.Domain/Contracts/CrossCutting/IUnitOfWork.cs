namespace DesafioHyperativa.Domain.Contracts.CrossCutting;
public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollBackAsync();
    bool IsInTransaction();

}
