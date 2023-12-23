using DesafioHyperativa.Domain.Contracts.Services.Base;
using DesafioHyperativa.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace DesafioHyperativa.Domain.Contracts.Services;
public interface ILotService : IService<Lot>
{
    Task SaveAsync(IFormFile formFile);
}
