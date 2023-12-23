using DesafioHyperativa.Domain.Contracts.Common;
using DesafioHyperativa.Domain.Contracts.CrossCutting;
using DesafioHyperativa.Domain.Contracts.Repositories;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using DesafioHyperativa.Service.Services;
using DesafioHyperativa.Test.Mocking;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioHyperativa.Test.Services;
public class UserServiceTest
{
    private UserService _userService;

    public UserServiceTest()
    {
        _userService = new UserService(new Mock<IUserRepository>().Object, new Mock<IUnitOfWork>().Object);
    }

    [Fact]
    public async Task GetByLoginAsync_SendingUserNullAsync()
    {
        var exceptions = await Assert.ThrowsAsync<BusinessException>(() => _userService.GetByLoginAsync(null));
        Assert.Equal("User not found.", exceptions.Message);
    }

    public static IEnumerable<object[]> UsersEmailNullOrEmpty()
    {
        yield return new object[] { new User() };
        yield return new object[] { new User() { Email = "" } };
    }

    [Theory]
    [MemberData(nameof(UsersEmailNullOrEmpty))]
    public async Task GetByLoginAsync_SendingUserEmailNullOrEmptyAsync(User user)
    {
        var exceptions = await Assert.ThrowsAsync<BusinessException>(() => _userService.GetByLoginAsync(user));
        Assert.Equal("Email not informed.", exceptions.Message);
    }

    public static IEnumerable<object[]> UserPasswordNullOrEmpty()
    {
        yield return new object[] { new User() { Email = "01@user.com" } };
        yield return new object[] { new User() { Email = "02@user.com", Password = "" } };
    }

    [Theory]
    [MemberData(nameof(UserPasswordNullOrEmpty))]
    public async Task GetByLoginAsync_SendingUserPasswordNullOrEmptyAsync(User user)
    {
        var exceptions = await Assert.ThrowsAsync<BusinessException>(() => _userService.GetByLoginAsync(user));
        Assert.Equal("Password not informed.", exceptions.Message);
    }

    [Fact]
    public async Task GetByLoginAsync_SendingValidUserButNotFoundAsync()
    {
        var repository = new Mock<IUserRepository>();
        repository
            .Setup(x => x.GetByLoginAsync(UserMock.UserNotRegistred))
            .ReturnsAsync(UserMock.ListUser.FirstOrDefault(x => x.Email == UserMock.UserNotRegistred.Email 
                            && x.Password == UserMock.UserNotRegistred.Password));

        var user = await _userService.GetByLoginAsync(UserMock.UserNotRegistred);
        Assert.Null(user);
    }

    [Fact]
    public async Task GetByLoginAsync_SendingValidUserAsync()
    {
        var repository = new Mock<IUserRepository>();
        repository
            .Setup(x => x.GetByLoginAsync(UserMock.UserRegistred))
            .ReturnsAsync(UserMock.ListUser.FirstOrDefault(x => x.Email == UserMock.UserRegistred.Email
                            && x.Password == UserMock.UserRegistred.Password));
        
        _userService = new UserService(repository.Object, new Mock<IUnitOfWork>().Object);
        
        var user = await _userService.GetByLoginAsync(UserMock.UserRegistred);
        Assert.Equal(user.Id, UserMock.UserRegistred.Id);
    }
}
