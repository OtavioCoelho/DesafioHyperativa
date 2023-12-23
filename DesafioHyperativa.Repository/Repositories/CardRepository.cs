using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Repository.Contexts;
using DesafioHyperativa.Repository.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DesafioHyperativa.Repository.Repositories;
public class CardRepository : Repository<Card>, ICardRepository
{
    public CardRepository(
        HyperativaDbContext context,
        IUnitOfWork unitOfWork,
        ILogger<Repository<Card>> logger) : base(context, unitOfWork, logger)
    {
    }

    public async Task<Card> GetByCardNumberAsync(string cardNumber)
    {
        return await this._dbSet
                .FirstOrDefaultAsync(x => x.Number == cardNumber);
    }
}
