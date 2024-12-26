using Moq;
using System.Security.Claims;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
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

    // [Fact]
    // public async Task CheckEventAsync_ReturnsEvent_WhenEventExists()
    // {
    //     // Arrange
    //     var eventId = 1;
    //     var eventToReturn = new Event { Id = eventId };
    //     _mockEventService.Setup(service => service.GetEventByIdAsync(eventId)).ReturnsAsync(eventToReturn);

    //     // Act
    //     var result = await _checkService.CheckEventAsync(eventId);

    //     // Assert
    //     Assert.Equal(eventToReturn, result);
    // }

    // [Fact]
    // public async Task CheckUserAsync_ReturnsNull_WhenUserNotFound()
    // {
    //     // Arrange
    //     var claimsPrincipal = new ClaimsPrincipal();
    //     _mockUserService.Setup(service => service.GetUserAsync(claimsPrincipal)).ReturnsAsync((IdentityUser)null);

    //     // Act
    //     var result = await _checkService.CheckUserAsync(claimsPrincipal);

    //     // Assert
    //     Assert.Null(result);
    //     _mockLogger.Verify(logger => logger.LogError(It.IsAny<string>()), Times.Once);
    // }

    // [Fact]
    // public async Task CheckUserAsync_ReturnsUser_WhenUserExists()
    // {
    //     // Arrange
    //     var claimsPrincipal = new ClaimsPrincipal();
    //     var userToReturn = new IdentityUser();
    //     _mockUserService.Setup(service => service.GetUserAsync(claimsPrincipal)).ReturnsAsync(userToReturn);

    //     // Act
    //     var result = await _checkService.CheckUserAsync(claimsPrincipal);

    //     // Assert
    //     Assert.Equal(userToReturn, result);
    // }
}
