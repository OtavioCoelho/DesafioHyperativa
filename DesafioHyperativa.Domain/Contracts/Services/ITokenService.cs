using DesafioHyperativa.Domain.Dto.Responses;
using DesafioHyperativa.Domain.Entities;

namespace DesafioHyperativa.Domain.Contracts.Services;
public interface ITokenService
{
    Task<CredentialDtoResponse> GenerateTokenAsync(User model);
}
