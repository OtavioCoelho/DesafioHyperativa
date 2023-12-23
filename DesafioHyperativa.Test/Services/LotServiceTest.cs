using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using DesafioHyperativa.Service.Services;
using DesafioHyperativa.Test.Mocking;
using Moq;

namespace DesafioHyperativa.Test.Services;
public class LotServiceTest
{
    private LotService _service;

    public LotServiceTest()
    {
        _service = new LotService(new Mock<IRepository<Lot>>().Object, new Mock<IUnitOfWork>().Object, new Mock<ICardService>().Object, new Mock<ILotRepository>().Object);
    }

    [Fact]
    public async Task SaveAsync_SendContentNullOrEmpty()
    {
        var exception = await Assert.ThrowsAsync<FileNotFoundException>(() => _service.SaveAsync(FormFileMock.contentErrorEmpty));
        Assert.Equal("Invalid or empty file.", exception.Message);

        exception = await Assert.ThrowsAsync<FileNotFoundException>(() => _service.SaveAsync(null));
        Assert.Equal("Invalid or empty file.", exception.Message);
    }

    [Fact]
    public async Task SaveAsync_SendContentDiffQuantityHeaderFooter()
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.SaveAsync(FormFileMock.contentErrorDiffQuantityHeaderFooter));
        Assert.Equal("The lot number and number records information in the header and footer is different.", exception.Message);
    }

    [Fact]
    public async Task SaveAsync_SendContentDiffLoteHeaderFooter()
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.SaveAsync(FormFileMock.contentErrorDiffLotHeaderFooter));
        Assert.Equal("The lot number and number records information in the header and footer is different.", exception.Message);
    }

    [Fact]
    public async Task SaveAsync_SendContentErrorDate()
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.SaveAsync(FormFileMock.contentErrorDate));
        Assert.Contains("Date the lot not informed or with formatting problems.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());

    }

    [Fact]
    public async Task SaveAsync_SendContentCardSmallSize()
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.SaveAsync(FormFileMock.contentErrorCardSmallSize));
        Assert.Contains("Number card is invalid.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Fact]
    public async Task SaveAsync_SendContentEmptyNameLot()
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.SaveAsync(FormFileMock.contentErrorEmptyNameLot));
        Assert.Contains("Lot number not informed.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());

    }

    [Fact]
    public async Task SaveAsync_SendContentQuantityZeroOrNotInforming()
    {
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.SaveAsync(FormFileMock.contentErrorQuantityZero));
        Assert.Contains("Number the records in the Lot, is not informed.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());

        exception = await Assert.ThrowsAsync<BusinessException>(() => _service.SaveAsync(FormFileMock.contentErrorQuantityNotInforming));
        Assert.Contains("Number the records in the Lot, is not informed.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Fact]
    public async Task SaveAsync_SendCorrect()
    {
        var exception = await Record.ExceptionAsync(() => _service.SaveAsync(FormFileMock.contentCorrect));
        Assert.Null(exception);
    }


}
