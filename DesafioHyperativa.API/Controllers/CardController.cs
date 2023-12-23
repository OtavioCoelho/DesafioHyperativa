using AutoMapper;
using DesafioHyperativa.API.Infra;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Dto.Requests;
using DesafioHyperativa.Domain.Dto.Responses;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace DesafioHyperativa.API.Controllers;

[Authorize]
[SwaggerResponse(StatusCodes.Status400BadRequest)]
[SwaggerResponse(StatusCodes.Status429TooManyRequests)]
[SwaggerResponse(StatusCodes.Status500InternalServerError)]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class CardController : CustomController
{
    private readonly ICardService _serviceCard;
    private readonly IMapper _mapper;
    public CardController(
        ILogger<CustomController> logger, ICardService serviceCard, IMapper mapper) : base(logger)
    {
        _serviceCard = serviceCard;
        _mapper = mapper;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Insert card", Description = "Insert card without the need for a lot.")]
    [SwaggerResponse(StatusCodes.Status201Created)]
    public async Task<ActionResult> Post(CardDtoRequest model)
    {
        if (!model.IsValid)
            throw new BusinessException(model.Errors);

        await _serviceCard.SaveAsync(_mapper.Map<Card>(model));
        return Created(nameof(Post), new { status = "Success" });
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get id card", Description = "Get identifier inique the card.")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get([FromQuery] CardDtoRequest model)
    {
        if (!model.IsValid)
            throw new BusinessException(model.Errors);

        var card = await _serviceCard.GetByCardNumberAsync(model.Number);
        if (card is null)
            return NotFound();
        return Ok(_mapper.Map<CardDtoResponse>(card));
    }
}
