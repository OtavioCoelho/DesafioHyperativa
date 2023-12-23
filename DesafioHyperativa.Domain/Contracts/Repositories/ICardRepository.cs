using DesafioHyperativa.Domain.Entities;

namespace DesafioHyperativa.Domain.Contracts.Repositories;
public interface ICardRepository : IRepository<Card>
{
    Task<Card> GetByCardNumberAsync(string cardNumber);
}
