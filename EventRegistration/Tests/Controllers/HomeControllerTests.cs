using System.Security.Claims;
using EventRegistration.Controllers;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EventRegistration.Tests.Controllers;

public class HomeControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IEventService> _mockEventService;
    private readonly Mock<ICheckService> _mockCheckService;
    private readonly Mock<IRegistrationService> _mockRegistrationService;
    private readonly Mock<ILogger<HomeController>> _mockLogger;

    private readonly HomeController _homeController;


    public HomeControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockEventService = new Mock<IEventService>();
        _mockRegistrationService = new Mock<IRegistrationService>();
        _mockCheckService = new Mock<ICheckService>();

        _mockLogger = new Mock<ILogger<HomeController>>();

        _homeController = new HomeController(_mockUserService.Object, _mockEventService.Object,
        _mockRegistrationService.Object, _mockCheckService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Index_RedirectWhenUserNotFound()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        _mockCheckService.Setup(s => s.CheckUserAsync(claimsPrincipal))
        .ReturnsAsync((IdentityUser)null);

        var result = await _homeController.Index();

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(AccountController.LoginRegister), redirectResult.ActionName);
        Assert.Equal("Account", redirectResult.ControllerName);
    }

    [Fact]
    public async Task Index_ReturnViewForRole()
    {
        var identityUser = new IdentityUser
        {
            UserName = "test",
            Email = "test"
        };
        var roles = new List<string> { "EventCreator" };
        var events = new List<EventViewModel>
        {
            new() {
                  Event = new Event
        {
            Id = 0,
            Name = "Test",
            Description = "Test",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = false,
            CreatorId = "test"
        },
        IsDrafted = false,
        IsRegistered = true
            }
        };
        var eventIdsByUser = new List<int> { 1 };

        _mockCheckService.Setup(s => s.CheckUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(identityUser);
        _mockUserService.Setup(s => s.GetRolesByUserAsync(identityUser)).ReturnsAsync(roles);
        _mockRegistrationService.Setup(s => s.GetEventIdsByUserIdAsync(identityUser.Id)).ReturnsAsync(eventIdsByUser);
        _mockEventService.Setup(s => s.GetUserEventsAsync(identityUser, roles, eventIdsByUser)).ReturnsAsync(events);

        var result = await _homeController.Index();
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);

        var model = Assert.IsAssignableFrom<List<EventViewModel>>(viewResult.Model);
        Assert.Equal(events.Count, model.Count);

    }
}