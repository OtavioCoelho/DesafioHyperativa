using DesafioHyperativa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioHyperativa.Domain.Contracts.Repositories;
public interface ILotRepository : IRepository<Lot>
{
    Task<Lot> GetAsync(string name, DateTime dateOperation, string lotNumber);
}
