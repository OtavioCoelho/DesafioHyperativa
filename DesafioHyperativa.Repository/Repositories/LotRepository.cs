using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Repository.Contexts;
using DesafioHyperativa.Repository.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DesafioHyperativa.Repository.Repositories;
public class LotRepository : Repository<Lot>, ILotRepository
{
    public LotRepository(
        HyperativaDbContext context,
        IUnitOfWork unitOfWork,
        ILogger<Repository<Lot>> logger) : base(context, unitOfWork, logger)
    {
    }

    public async Task<Lot> GetAsync(string name, DateTime dateOperation, string lotNumber)
    {
        return await this._dbSet.FirstOrDefaultAsync(x => x.Name == name &&
            x.DateOperation.Date == dateOperation.Date &&
            x.LotNumber == lotNumber);
    }
}
