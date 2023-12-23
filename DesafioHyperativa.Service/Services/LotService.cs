using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using DesafioHyperativa.Service.Services.Base;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Text;

namespace DesafioHyperativa.Service.Services;
public class LotService : Service<Lot>, ILotService
{
    private readonly ICardService _cardService;
    private readonly ILotRepository _lotRepository;
    public LotService(IRepository<Lot> repository,
        IUnitOfWork unitOfWork,
        ICardService cardService,
        ILotRepository lotRepository) : base(repository, unitOfWork)
    {
        _cardService = cardService;
        _lotRepository = lotRepository;
    }
    public async Task SaveAsync(IFormFile formFile)
    {
        var listLines = await ReadFormFile(formFile);
        Lot lot = await ImportLotCards(listLines);
        await SaveAsync(lot);
    }

    #region Private Methods
    private async Task<Lot> ImportLotCards(IList<string> listLines)
    {
        try
        {
            Lot lot = await FillLotData(listLines);
            lot.Cards = await FillCardsData(listLines);
            if (lot.NumberRecords != lot.Cards.Count)
                throw new BusinessException("The number of cards reported in the lot header is different from the total number of cards the lot.");
            return lot;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<HashSet<Card>> FillCardsData(IList<string> listLines)
    {
        HashSet<Card> cards = new();
        foreach (var line in listLines)
        {
            int lineSize = line.Length;
            Card card = new()
            {
                LineIdentifier = line.Substring(0, 1),
                Number = line.Substring(7, 19).Trim()
            };
            if (int.TryParse(line.Substring(1, 6).Trim(), out int lotNumber))
                card.LotNumber = lotNumber.ToString();
            if (!card.IsValid())
                throw new BusinessException(card.GetErros());

            var cardSaved = await _cardService.GetByCardNumberAsync(card.Number);
            if (cardSaved != null)
                throw new BusinessException("In the lot provided, there are cards that have already been saved.");
            if (cards.Any(x => x.Number.Equals(card.Number, StringComparison.CurrentCultureIgnoreCase)))
                throw new BusinessException("The lot you entered contains duplicate cards numbers.");
            cards.Add(card);
        }
        return cards;
    }

    private async Task<Lot> FillLotData(IList<string> listLines)
    {
        Lot lot = new();
        string firstLine = listLines.FirstOrDefault();
        listLines.Remove(firstLine);
        string lastLine = listLines.LastOrDefault();
        listLines.Remove(lastLine);

        if (!listLines.Any())
            throw new BusinessException("Lot not contains cards.");
        if (string.IsNullOrEmpty(lastLine) || string.IsNullOrEmpty(firstLine))
            throw new BusinessException("Error when processing lot of cards.");


        lot.Name = firstLine.Substring(0, 29).Trim();
        if (DateTime.TryParseExact(firstLine.Substring(29, 8), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOperattion))
            lot.DateOperation = dateOperattion;
        lot.LotNumber = firstLine.Substring(37, 8).Trim();
        if (int.TryParse(firstLine.Substring(45, 6).Trim(), out int numberRecords))
            lot.NumberRecords = numberRecords;

        if (!lot.IsValid())
            throw new BusinessException(lot.GetErros());

        var lotNumberLastLine = lastLine.Substring(0, 8).Trim();
        int.TryParse(lastLine.Substring(8, 6).Trim(), out int numberRecordsLastLine);

        if (!lot.LotNumber.Equals(lotNumberLastLine) ||
            lot.NumberRecords != numberRecordsLastLine)
            throw new BusinessException("The lot number and number records information in the header and footer is different.");

        if (await _lotRepository.GetAsync(lot.Name, lot.DateOperation, lot.LotNumber) != null)
            throw new BusinessException("There is a saved lot with the same information as the batch informed.");
        return lot;
    }

    private async Task<IList<string>> ReadFormFile(IFormFile formFile)
    {
        if (formFile == null || formFile.Length <= 0)
            throw new FileNotFoundException("Invalid or empty file.");

        try
        {
            using (var streamReader = new StreamReader(formFile.OpenReadStream(), Encoding.UTF8))
            {
                var listLines = new List<string>();
                while (!streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Length > 51)
                            line = line.Substring(0, 51);
                        else
                            line = line.PadRight(51, ' ');
                        listLines.Add(line);
                    }
                }
                return listLines;
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new BusinessException($"Unauthorized file access error: {ex.Message}");
        }
        catch (IOException ex)
        {
            throw new BusinessException($"I/O error: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error reading the file: {ex.Message}");
        }
    }
    #endregion
}
