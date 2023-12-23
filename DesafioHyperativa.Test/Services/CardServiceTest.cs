using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using DesafioHyperativa.Repository.Repositories;
using DesafioHyperativa.Service.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioHyperativa.Test.Services;
public class CardServiceTest
{
    private CardService _cardService;

    public CardServiceTest()
    {
        _cardService = new CardService(new Mock<ICardRepository>().Object,
            new Mock<IUnitOfWork>().Object);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task GetByCardNumberAsync_SendNullOrEmptyValue(string? cardNumber)
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _cardService.GetByCardNumberAsync(cardNumber));
        Assert.Equal("Card number invalid.", exception.Message);
    }

    [Theory]
    [InlineData("44A6897999999999")]
    [InlineData("4456B97999999999")]
    [InlineData("445689C999999999")]
    [InlineData("44568979D9999999")]
    [InlineData("4456897999E99999")]
    [InlineData("4456897999F99999")]
    [InlineData("4456897999999G99")]

    public async Task GetByCardNumberAsync_SendValueWithLetter(string cardNumber)
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _cardService.GetByCardNumberAsync(cardNumber));
        Assert.Equal("The card number entered contains letters along with it.", exception.Message);
    }

    [Theory]
    [InlineData("44568979999999994456897999999999")]
    [InlineData("44568979229699994456897922969999")]
    [InlineData("44568979981999994456897998199999")]
    [InlineData("44568979129999994456897912999999")]
    [InlineData("44568979199999994456897919999999")]
    [InlineData("44568979990999994456897999099999")]
    public async Task GetByCardNumberAsync_SendInvalidNumber(string cardNumber)
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _cardService.GetByCardNumberAsync(cardNumber));
        Assert.Equal("The card number is invalid, too long, not standard.", exception.Message);
    }

    [Theory]
    [InlineData("4456897999999999")]
    [InlineData("4456897922969999")]
    [InlineData("4456897998199999")]
    [InlineData("4456897912999999")]
    [InlineData("4456897919999999")]
    [InlineData("4456897999099999")]
    public async Task GetByCardNumberAsync_SendValidValue(string cardNumber)
    {
        List<Card> cards = new()
        {
            new (){
                Id = Guid.NewGuid(),
                DtRegister = DateTime.Now,
                DtUpdate = DateTime.Now,
                Number = "4456897999999999"
            },
             new (){
                Id = Guid.NewGuid(),
                DtRegister = DateTime.Now,
                DtUpdate = DateTime.Now,
                Number = "4456897922969999"
            },
              new (){
                Id = Guid.NewGuid(),
                DtRegister = DateTime.Now,
                DtUpdate = DateTime.Now,
                Number = "4456897998199999"
            },
               new (){
                Id = Guid.NewGuid(),
                DtRegister = DateTime.Now,
                DtUpdate = DateTime.Now,
                Number = "4456897912999999"
            },
                new (){
                Id = Guid.NewGuid(),
                DtRegister = DateTime.Now,
                DtUpdate = DateTime.Now,
                Number = "4456897919999999"
            },
                 new (){
                Id = Guid.NewGuid(),
                DtRegister = DateTime.Now,
                DtUpdate = DateTime.Now,
                Number = "4456897999099999"
            },

        };

        var cardRepository = new Mock<ICardRepository>();
        cardRepository
            .Setup(x => x.GetByCardNumberAsync(cardNumber))
            .ReturnsAsync(cards.FirstOrDefault(x => x.Number.Equals(cardNumber, StringComparison.CurrentCultureIgnoreCase)));
        
        _cardService = new CardService(cardRepository.Object,new Mock<IUnitOfWork>().Object);
        var card = await _cardService.GetByCardNumberAsync(cardNumber);
        Assert.Equal(cardNumber, card.Number);
    }
}
