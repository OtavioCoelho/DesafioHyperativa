using DesafioHyperativa.API.Infra;
using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using DesafioHyperativa.Test.Mocking;
using Moq;

namespace DesafioHyperativa.Test.Services;
public class TokenServiceTest
{
    private TokenService _tokenService;

    public TokenServiceTest()
    {
        _tokenService = new TokenService(new Mock<IUserService>().Object);
    }

    [Fact]
    public async Task GenerateToken_SendUserNull()
    {
        var userService = new Mock<IUserService>();
        userService.Setup(x => x.ValidateUserGetByLogin(null)).ThrowsAsync(new BusinessException("User not found."));
        _tokenService = new TokenService(userService.Object);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _tokenService.GenerateTokenAsync(null));
        Assert.Equal("User not found.", exception.Message);
    }

    public static IEnumerable<object[]> UserWithEmailNullOrEmpty()
    {
        yield return new object[] { new User() };
        yield return new object[] { new User() { Email = string.Empty } };
    }
    [Theory]
    [MemberData(nameof(UserWithEmailNullOrEmpty))]
    public async Task GenerateToken_SendUserWithEmailNullOrEmpty(User user )
    {
        var userService = new Mock<IUserService>();
        userService.Setup(x => x.ValidateUserGetByLogin(user)).ThrowsAsync(new BusinessException("Email not informed."));
        _tokenService = new TokenService(userService.Object);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _tokenService.GenerateTokenAsync(user));
        Assert.Equal("Email not informed.", exception.Message);

        exception = await Assert.ThrowsAsync<BusinessException>(() => _tokenService.GenerateTokenAsync(user));
        Assert.Equal("Email not informed.", exception.Message);
    }

    public static IEnumerable<object[]> UserWitPasswordNullOrEmpty()
    {
        yield return new object[] { new User() { Email = "01@user.com" }  };
        yield return new object[] { new User() { Email = "01@user.com", Password = string.Empty } };
    }
    [Theory]
    [MemberData(nameof(UserWitPasswordNullOrEmpty))]
    public async Task GenerateToken_SendUserWitPasswordNullOrEmpty(User user)
    {
        var userService = new Mock<IUserService>();
        userService.Setup(x => x.ValidateUserGetByLogin(user)).ThrowsAsync(new BusinessException("Password not informed."));
        _tokenService = new TokenService(userService.Object);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _tokenService.GenerateTokenAsync(user));
        Assert.Equal("Password not informed.", exception.Message);

        exception = await Assert.ThrowsAsync<BusinessException>(() => _tokenService.GenerateTokenAsync(user));
        Assert.Equal("Password not informed.", exception.Message);
    }

    [Fact]
    public async Task GenerateToken_SendUserValid()
    {
        var userTest = new User() { Email = "01@user.com", Password = "Abcd1234" };

        var userService = new Mock<IUserService>();
        userService
            .Setup(x => x.GetByLoginAsync(userTest))
            .ReturnsAsync(UserMock.UserRegistred);
        _tokenService = new TokenService(userService.Object);

        var credentials = await _tokenService.GenerateTokenAsync(userTest);
        Assert.NotNull(credentials);
        Assert.NotNull(credentials.AccessToken);
        Assert.NotEqual(credentials.ExpiresIn, DateTime.MinValue);
        Assert.NotNull(credentials.TokenType);
    }
}
