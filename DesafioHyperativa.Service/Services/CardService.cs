using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using DesafioHyperativa.Service.Services.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DesafioHyperativa.Service.Services;
public class CardService : Service<Card>, ICardService
{
    private readonly ICardRepository _repositoryCard;
    public CardService(
        ICardRepository repository,
        IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
        _repositoryCard = repository;
    }

    public async Task<Card> GetByCardNumberAsync(string cardNumber)
    {
        Validate(cardNumber);
        return await _repositoryCard.GetByCardNumberAsync(cardNumber);
    }

    private void Validate(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber))
            throw new BusinessException("Card number invalid.");
        if (cardNumber.Any(x => !char.IsNumber(x)))
            throw new BusinessException("The card number entered contains letters along with it.");
        if (cardNumber.Length > 19)
            throw new BusinessException("The card number is invalid, too long, not standard.");



    }
}
