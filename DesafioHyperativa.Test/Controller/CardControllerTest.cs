using AutoMapper;
using DesafioHyperativa.API.Controllers;
using DesafioHyperativa.API.Infra;
using DesafioHyperativa.API.Mappings;
using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using DesafioHyperativa.Service.Services;
using DesafioHyperativa.Test.Mocking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesafioHyperativa.Domain.Dto.Requests;

namespace DesafioHyperativa.Test.Controller;
public class CardControllerTest
{
    private CardController _controller;
    private IMapper _mapper;

    public CardControllerTest()
    {
        var autoMapperProfile = new CardProfileMapper();
        var _configurationMapper = new MapperConfiguration(x => x.AddProfile(autoMapperProfile));
        _mapper = new Mapper(_configurationMapper);

        var _service = new Mock<ICardService>();
        _service.Setup(x => x.SaveAsync(It.IsAny<Card>())).ReturnsAsync(new Card() { Number = CardMock.CardOnlyNumberValid });
        _controller = new CardController(new Mock<ILogger<CustomController>>().Object, _service.Object, _mapper);
    }

    #region Post

    [Theory]
    [InlineData("")]
    public async Task Post_SendEmptyValue(string cardNumber)
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.Post(new CardDtoRequest() { Number = cardNumber }));
        Assert.Contains("Number card is not informed.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Theory]
    [InlineData("44A6897999999999")]
    [InlineData("4456B97999999999")]
    [InlineData("445689C999999999")]
    [InlineData("44568979D9999999")]
    [InlineData("4456897999E99999")]
    [InlineData("4456897999F99999")]
    [InlineData("4456897999999G99")]

    public async Task Post_SendValueWithLetter(string cardNumber)
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.Post(new CardDtoRequest() { Number = cardNumber }));
        Assert.Contains("Number card is invalid.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Theory]
    [InlineData("44568979999999994456897999999999")]
    [InlineData("44568979229699994456897922969999")]
    [InlineData("44568979981999994456897998199999")]
    [InlineData("44568979129999994456897912999999")]
    [InlineData("44568979199999994456897919999999")]
    [InlineData("44568979990999994456897999099999")]
    public async Task Post_SendInvalidNumber(string cardNumber)
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.Post(new CardDtoRequest() { Number = cardNumber }));
        Assert.Contains("Number card is invalid.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Theory]
    [InlineData("4456897999999999")]
    [InlineData("4456897922969999")]
    [InlineData("4456897998199999")]
    [InlineData("4456897912999999")]
    [InlineData("4456897919999999")]
    [InlineData("4456897999099999")]
    public async Task Post_SendValidValue(string cardNumber)
    {
        var result = await _controller.Post(new CardDtoRequest() { Number = cardNumber });
        Assert.IsType<CreatedResult>(result);
    }
    #endregion

    [Fact]
    public async Task Get_SendCardNumberEmpty()
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.Get(new CardDtoRequest() { Number = string.Empty }));
        Assert.Contains("Number card is not informed.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Fact]
    public async Task Get_SendCardNotFound()
    {
        var _service = new Mock<ICardService>();
        _service.Setup(x => x.GetByCardNumberAsync(It.IsAny<string>())).ReturnsAsync(CardMock.ListCards.FirstOrDefault(x => x.Number == CardMock.CardOnlyNumberNotRegistred));
        _controller = new CardController(new Mock<ILogger<CustomController>>().Object, _service.Object, _mapper);

        var result = await _controller.Get(new CardDtoRequest() { Number = CardMock.CardOnlyNumberNotRegistred });
        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task Get_SendCardNumberRegistred()
    {
        var _service = new Mock<ICardService>();
        _service.Setup(x => x.GetByCardNumberAsync(It.IsAny<string>())).ReturnsAsync(CardMock.ListCards.FirstOrDefault(x => x.Number == CardMock.CardOnlyNumberValid));
        _controller = new CardController(new Mock<ILogger<CustomController>>().Object, _service.Object, _mapper);

        var result =  await _controller.Get(new CardDtoRequest() { Number = CardMock.CardOnlyNumberValid });

        Assert.IsType<OkObjectResult>(result);
    }
}
