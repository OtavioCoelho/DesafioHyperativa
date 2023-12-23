using AutoMapper;
using DesafioHyperativa.Domain.Dto.Requests;
using DesafioHyperativa.Domain.Dto.Responses;
using DesafioHyperativa.Domain.Entities;

namespace DesafioHyperativa.API.Mappings;

public class CardProfileMapper : Profile
{
    public CardProfileMapper()
    {
        CreateMap<CardDtoRequest, Card>();
        CreateMap<Card, CardDtoResponse>();
    }
}
