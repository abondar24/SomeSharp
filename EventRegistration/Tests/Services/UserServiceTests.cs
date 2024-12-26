using System.Security.Claims;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace EventRegistration.Tests.Services;

public class UserServiceTests
{

    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
    private readonly Mock<SignInManager<IdentityUser>> _signInManagerMock;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userManagerMock = new Mock<UserManager<IdentityUser>>(
                             Mock.Of<IUserStore<IdentityUser>>(),
                             null, null, null, null, null, null, null, null
                             );

        _signInManagerMock = new Mock<SignInManager<IdentityUser>>(
            _userManagerMock.Object,
            Mock.Of<HttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
            null, null, null, null
        );

        _userService = new UserService(_userManagerMock.Object, _signInManagerMock.Object);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldSuccessfullyCreateUser()
    {
        var model = new RegisterViewModel
        {
            Email = "test",
            Password = "pass",
            Role = "EventParticipant"
        };

        var identityUser = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
        .ReturnsAsync(IdentityResult.Success);

        _userManagerMock.Setup(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
        .ReturnsAsync(IdentityResult.Success);

        _signInManagerMock.Setup(m => m.SignInAsync(It.IsAny<IdentityUser>(), false, null))
                                  .Returns(Task.CompletedTask);

        var result = await _userService.CreateUserAsync(model);

        Assert.True(result);


        _userManagerMock.Verify(m => m.CreateAsync(It.Is<IdentityUser>(u => u.Email == model.Email), model.Password), Times.Once);
        _userManagerMock.Verify(m => m.AddToRoleAsync(It.Is<IdentityUser>(u => u.Email == model.Email), model.Role), Times.Once);
        _signInManagerMock.Verify(m => m.SignInAsync(It.Is<IdentityUser>(u => u.Email == model.Email), false, null), Times.Once);
    }


    [Fact]
    public async Task GetUserAsync_ShouldReturnUser()
    {
        var identityUser = new IdentityUser
        {
            UserName = "test",
            Email = "test"
        };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
       {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id")
        }));

        _userManagerMock.Setup(m => m.GetUserAsync(claimsPrincipal))
        .ReturnsAsync(identityUser);


        var result = await _userService.GetUserAsync(claimsPrincipal);
        Assert.NotNull(result);
        Assert.Equal(identityUser.Id, result?.Id);
        Assert.Equal(identityUser.UserName, result?.UserName);
        Assert.Equal(identityUser.Email, result?.Email);
    }


    [Fact]
    public async Task GetUserByEmailAsync_ShouldReturnUser()
    {
        var identityUser = new IdentityUser
        {
            UserName = "test",
            Email = "test"
        };


        _userManagerMock.Setup(m => m.FindByEmailAsync(identityUser.Email))
        .ReturnsAsync(identityUser);


        var result = await _userService.GetUserByEmailAsync(identityUser.Email);
        Assert.NotNull(result);
        Assert.Equal(identityUser.Email, result?.Email);
    }

    [Fact]
    public async Task GetRolesByUserAsync_ShouldReturnUserRoles()
    {
        var identityUser = new IdentityUser
        {
            UserName = "test",
            Email = "test"
        };

        var roles = new List<string> { "EventParticipant" };


        _userManagerMock.Setup(m => m.GetRolesAsync(identityUser))
        .ReturnsAsync(roles);


        var result = await _userService.GetRolesByUserAsync(identityUser);
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(roles[0], result[0]);
    }


    [Fact]
    public async Task PasswordSignInAsync_ShouldSuccessfullySignIn()
    {
        var model = new LoginViewModel
        {
            Email = "test",
            Password = "test",
            RememberMe = false
        };

        _signInManagerMock.Setup(m => m.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true))
                               .ReturnsAsync(SignInResult.Success);


        var result = await _userService.PasswordSignInAsync(model);
        Assert.True(result.Succeeded);
        _signInManagerMock.Verify(m => m.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true), Times.Once);
    }


    [Fact]
    public async Task SignOutAsyng_ShouldSuccessfullySignOut()
    {
        _signInManagerMock.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

        await _userService.SignOutAsync();

        _signInManagerMock.Verify(m => m.SignOutAsync(), Times.Once);
    }
}