using AutoMapper;
using DesafioHyperativa.API.Infra;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Dto.Requests;
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
public class LotController : CustomController
{
    private readonly ILotService _serviceLot;
    private readonly IMapper _mapper;
    public LotController(
        ILogger<CustomController> logger,
        ILotService serviceLot,
        IMapper mapper) : base(logger)
    {
        _serviceLot = serviceLot;
        _mapper = mapper;
    }


    [HttpPost]
    [SwaggerOperation(Summary = "Insert Lot of cards", Description = "Insert from file (.txt) lot of cards.")]
    [SwaggerResponse(StatusCodes.Status201Created)]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> Post([FromForm] LotDtoRequest model)
    {
        if (!model.IsValid)
            throw new BusinessException(model.Errors);

        await _serviceLot.SaveAsync(model.File);
        return CreatedAtAction(nameof(Post), new { status = "Success" });
    }

}
