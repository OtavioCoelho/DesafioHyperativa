using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using DesafioHyperativa.API.Controllers;
using DesafioHyperativa.API.Infra;
using DesafioHyperativa.API.Mappings;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Dto.Requests;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using DesafioHyperativa.Test.Mocking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DesafioHyperativa.Test.Controller;
public class AuthorizeControllerTest
{
    private AuthorizeController _controller;
    private IMapper _mapper;
    public AuthorizeControllerTest()
    {
        var autoMapperProfile = new AuthorizeProfileMapper();
        var _configurationMapper = new MapperConfiguration(x => x.AddProfile(autoMapperProfile));
        _mapper = new Mapper(_configurationMapper);

        _controller = new AuthorizeController(new Mock<ILogger<CustomController>>().Object,
                                              new Mock<ITokenService>().Object,
                                              _mapper);
    }

    [Fact]
    public async Task GenerateToken_SendCredentialsEmailEmpty()
    {
        var service = new Mock<ITokenService>();
        service.Setup(m => m.GenerateTokenAsync(UserMock.UserRegistred))
            .ReturnsAsync(CredentialDtoResponseMock.CredentialsValid);

        _controller = new(
            new Mock<ILogger<CustomController>>().Object,
            service.Object,
            _mapper);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.GenerateToken(CredentialDtoRequestMock.CredentialsEmailEmpty));
        Assert.Contains("Enter your e-mail.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());


        exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.GenerateToken(CredentialDtoRequestMock.CredentialsEmailNull));
        Assert.Contains("Enter your e-mail.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Fact]
    public async Task GenerateToken_SendCredentialsEmailNull()
    {
        var service = new Mock<ITokenService>();
        service.Setup(m => m.GenerateTokenAsync(UserMock.UserRegistred))
            .ReturnsAsync(CredentialDtoResponseMock.CredentialsValid);

        _controller = new(
            new Mock<ILogger<CustomController>>().Object,
            service.Object,
            _mapper);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.GenerateToken(CredentialDtoRequestMock.CredentialsEmailNull));
        Assert.Contains("Enter your e-mail.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Fact]
    public async Task GenerateToken_SendCredentialsPasswordEmpty()
    {
        var service = new Mock<ITokenService>();
        service.Setup(m => m.GenerateTokenAsync(UserMock.UserRegistred))
            .ReturnsAsync(CredentialDtoResponseMock.CredentialsValid);

        _controller = new(
            new Mock<ILogger<CustomController>>().Object,
            service.Object,
            _mapper);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.GenerateToken(CredentialDtoRequestMock.CredentialsPasswordEmpty));
        Assert.Contains("Enter your password.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Fact]
    public async Task GenerateToken_SendCredentialsPasswordNull()
    {
        var service = new Mock<ITokenService>();
        service.Setup(m => m.GenerateTokenAsync(UserMock.UserRegistred))
            .ReturnsAsync(CredentialDtoResponseMock.CredentialsValid);

        _controller = new(
            new Mock<ILogger<CustomController>>().Object,
            service.Object,
            _mapper);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.GenerateToken(CredentialDtoRequestMock.CredentialsPasswordNull));
        Assert.Contains("Enter your password.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Fact]
    public async Task GenerateToken_SendCredentialsPasswordWithoutUpperLetter()
    {
        var service = new Mock<ITokenService>();
        service.Setup(m => m.GenerateTokenAsync(UserMock.UserRegistred))
            .ReturnsAsync(CredentialDtoResponseMock.CredentialsValid);

        _controller = new(
            new Mock<ILogger<CustomController>>().Object,
            service.Object,
            _mapper);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.GenerateToken(CredentialDtoRequestMock.CredentialsPasswordWithoutUpperLetter));
        Assert.Contains("The password must contain at least 1 uppercase letter, 1 lowercase letter and 1 number.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Fact]
    public async Task GenerateToken_SendCredentialsPasswordWithoutLowerLetter()
    {
        var service = new Mock<ITokenService>();
        service.Setup(m => m.GenerateTokenAsync(UserMock.UserRegistred))
            .ReturnsAsync(CredentialDtoResponseMock.CredentialsValid);

        _controller = new(
            new Mock<ILogger<CustomController>>().Object,
            service.Object,
            _mapper);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.GenerateToken(CredentialDtoRequestMock.CredentialsPasswordWithoutLowerLetter));
        Assert.Contains("The password must contain at least 1 uppercase letter, 1 lowercase letter and 1 number.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Fact]
    public async Task GenerateToken_SendCredentialsPasswordWithoutMinimum8Character()
    {
        var service = new Mock<ITokenService>();
        service.Setup(m => m.GenerateTokenAsync(UserMock.UserRegistred))
            .ReturnsAsync(CredentialDtoResponseMock.CredentialsValid);

        _controller = new(
            new Mock<ILogger<CustomController>>().Object,
            service.Object,
            _mapper);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.GenerateToken(CredentialDtoRequestMock.CredentialsPasswordWithoutMinimum8Character));
        Assert.Contains("The password must be between 8 and 16 characters long.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }
    [Fact]
    public async Task GenerateToken_SendCredentialsPasswordWithoutMaximum16Character()
    {
        var service = new Mock<ITokenService>();
        service.Setup(m => m.GenerateTokenAsync(UserMock.UserRegistred))
            .ReturnsAsync(CredentialDtoResponseMock.CredentialsValid);

        _controller = new(
            new Mock<ILogger<CustomController>>().Object,
            service.Object,
            _mapper);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.GenerateToken(CredentialDtoRequestMock.CredentialsPasswordWithoutMaximum16Character));
        Assert.Contains("The password must be between 8 and 16 characters long.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }
    [Fact]
    public async Task GenerateToken_SendCredentialsPasswordWithoutCharacterNumeric()
    {
        var service = new Mock<ITokenService>();
        service.Setup(m => m.GenerateTokenAsync(UserMock.UserRegistred))
            .ReturnsAsync(CredentialDtoResponseMock.CredentialsValid);

        _controller = new(
            new Mock<ILogger<CustomController>>().Object,
            service.Object,
            _mapper);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.GenerateToken(CredentialDtoRequestMock.CredentialsPasswordWithoutCharacterNumeric));
        Assert.Contains("The password must contain at least 1 uppercase letter, 1 lowercase letter and 1 number.", exception?.Data["Message"] as string[] ?? Array.Empty<string>());
    }

    [Fact]
    public async Task GenerateToken_SendCredentialsValid()
    {
        var service = new Mock<ITokenService>();
        service.Setup(m => m.GenerateTokenAsync(UserMock.UserRegistred))
            .ReturnsAsync(CredentialDtoResponseMock.CredentialsValid);

        _controller = new(
            new Mock<ILogger<CustomController>>().Object,
            service.Object,
            _mapper);

        var result = await _controller.GenerateToken(CredentialDtoRequestMock.CredentialsValid);

        Assert.IsType<OkObjectResult>(result);
    }
}
