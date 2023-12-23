using DesafioHyperativa.Domain.Contracts.Services.Base;
using DesafioHyperativa.Domain.Entities;

namespace DesafioHyperativa.Domain.Contracts.Services;
public interface ICardService : IService<Card>
{
    Task<Card> GetByCardNumberAsync(string cardNumber);
}
