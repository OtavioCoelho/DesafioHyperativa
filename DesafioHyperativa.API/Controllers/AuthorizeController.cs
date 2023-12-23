using AutoMapper;
using DesafioHyperativa.API.Infra;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Dto.Requests;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace DesafioHyperativa.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[SwaggerResponse(StatusCodes.Status400BadRequest)]
[SwaggerResponse(StatusCodes.Status429TooManyRequests)]
[SwaggerResponse(StatusCodes.Status500InternalServerError)]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class AuthorizeController : ControllerBase
{
    private readonly ILogger<CustomController> _logger;
    private readonly ITokenService _serviceToken;
    private readonly IMapper _mapper;

    public AuthorizeController(ILogger<CustomController> logger,
        ITokenService serviceToken,
        IMapper map)
    {
        _logger = logger;
        _serviceToken = serviceToken;
        _mapper = map;
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Generate token", Description = "Gerenate token using access API")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    public async Task<ActionResult> GenerateToken(CredentialDtoRequest model)
    {
        if (!model.IsValid)
            throw new BusinessException(model.Errors);
        var token = await _serviceToken.GenerateTokenAsync(_mapper.Map<User>(model));
        return Ok(token);
    }
}
