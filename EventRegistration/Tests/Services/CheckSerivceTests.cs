using Moq;
using System.Security.Claims;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace EventRegistration.Tests;
public class CheckServiceTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IEventService> _mockEventService;
    private readonly Mock<ILogger<CheckService>> _mockLogger;
    private readonly ICheckService _checkService;

    public CheckServiceTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockEventService = new Mock<IEventService>();
        _mockLogger = new Mock<ILogger<CheckService>>();

        _checkService = new CheckService(_mockUserService.Object, _mockEventService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task CheckEventAsync_ReturnsNull_WhenEventNotFound()
    {
        var eventId = 1;
        _mockEventService.Setup(service => service.GetEventByIdAsync(eventId)).ReturnsAsync((Event)null);

        var result = await _checkService.CheckEventAsync(eventId);
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckEventAsync_ReturnsEvent_WhenEventExists()
    {
        var @event = new Event
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = false,
            CreatorId = "test"

        };
        _mockEventService.Setup(service => service.GetEventByIdAsync(@event.Id)).ReturnsAsync(@event);

        var result = await _checkService.CheckEventAsync(@event.Id);
        Assert.Equal(@event, result);
    }

    [Fact]
    public async Task CheckUserAsync_ReturnsNull_WhenUserNotFound()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        _mockUserService.Setup(service => service.GetUserAsync(claimsPrincipal)).ReturnsAsync((IdentityUser)null);

        var result = await _checkService.CheckUserAsync(claimsPrincipal);
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckUserAsync_ReturnsUser_WhenUserExists()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        var userToReturn = new IdentityUser();
        _mockUserService.Setup(service => service.GetUserAsync(claimsPrincipal)).ReturnsAsync(userToReturn);

        var result = await _checkService.CheckUserAsync(claimsPrincipal);
        Assert.Equal(userToReturn, result);
    }
}
