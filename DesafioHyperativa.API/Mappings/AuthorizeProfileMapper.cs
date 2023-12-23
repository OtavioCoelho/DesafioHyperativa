using AutoMapper;
using DesafioHyperativa.Domain.Dto.Requests;
using DesafioHyperativa.Domain.Entities;

namespace DesafioHyperativa.API.Mappings;

public class AuthorizeProfileMapper : Profile
{
    public AuthorizeProfileMapper()
    {
        CreateMap<CredentialDtoRequest, User>();
    }
}
