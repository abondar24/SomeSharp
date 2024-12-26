using EventRegistration.Controllers;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EventRegistration.Tests.Controllers;

public class AccountControllerTests
{
    private readonly Mock<IUserService> _mockUserService;

    private readonly Mock<ILogger<AccountController>> _mockLogger;

    private readonly AccountController _accountController;

    public AccountControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockLogger = new Mock<ILogger<AccountController>>();

        _accountController = new AccountController(_mockUserService.Object, _mockLogger.Object);
    }

    [Fact]
    public void Login_GET_Returns_ViewResult()
    {
        var result = _accountController.Login();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Register_GET_Returns_ViewResult()
    {
        var result = _accountController.Register();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void LoginRegister_GET_Returns_ViewResult()
    {
        var result = _accountController.LoginRegister();

        Assert.IsType<ViewResult>(result);
    }


    [Fact]
    public async Task Login_POST_RedirectsToHomeIndex_When_ModelIsValid()
    {
        var model = new LoginViewModel
        {
            Email = "test@example.com",
            Password = "password",
            RememberMe = true
        };

        var identityUser = new IdentityUser
        {
            UserName = "test",
            Email = "test"
        };

        _mockUserService.Setup(s => s.PasswordSignInAsync(model)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
        _mockUserService.Setup(s => s.GetUserByEmailAsync(model.Email)).ReturnsAsync(identityUser);
        _mockUserService.Setup(s => s.GetRolesByUserAsync(identityUser)).ReturnsAsync(["EventCreator"]);

        var result = await _accountController.Login(model);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }

    [Fact]
    public async Task Login_POST_Return_ViewResult_When_ModelIsInvalid()
    {
        var model = new LoginViewModel
        {
            Email = "test@example.com",
            RememberMe = true
        };
        _accountController.ModelState.AddModelError("Password", "Password is missing");


        var result = await _accountController.Login(model);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(model, viewResult.Model);
    }

    [Fact]
    public async Task Login_POST_UserNotFound_RedirectsToLoginRegister()
    {
        var model = new LoginViewModel
        {
            Email = "test@example.com",
            Password = "password",
            RememberMe = true
        };

        _mockUserService.Setup(s => s.PasswordSignInAsync(model)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
        _mockUserService.Setup(s => s.GetUserByEmailAsync(model.Email)).ReturnsAsync((IdentityUser)null);

        // Act
        var result = await _accountController.Login(model);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(AccountController.LoginRegister), redirectResult.ActionName);
        Assert.Equal("Account", redirectResult.ControllerName);
    }

    [Fact]
    public async Task Logout_POST_RedirectsToHome()
    {
        _mockUserService.Setup(s => s.SignOutAsync()).Returns(Task.CompletedTask);

        var result = await _accountController.Logout();

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }

    [Fact]
    public async Task Register_POST_User_Created()
    {
        var model = new RegisterViewModel
        {
            Email = "user@example.com",
            Password = "password",
            ConfirmPassword = "password",
            Role = "EventParticipant"
        };

        _mockUserService.Setup(s => s.CreateUserAsync(model)).ReturnsAsync(true);

        var result = await _accountController.Register(model);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }


    [Fact]
    public async Task Register_POST_Return_ViewResult_When_ModelIsInvalid()
    {
        var model = new RegisterViewModel
        {
            Email = "user@example.com",
            Password = "password",
            ConfirmPassword = "password",
        };

        _accountController.ModelState.AddModelError("Role", "Role is missing");

        var result = await _accountController.Register(model);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(model, viewResult.Model);
    }
}