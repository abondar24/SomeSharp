using System.Security.Claims;
using EventRegistration.Controllers;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EventRegistration.Tests.Controllers;


public class ParticipantControllerTests
{
    private readonly Mock<IEventService> _mockEventService;
    private readonly Mock<ICheckService> _mockCheckService;
    private readonly Mock<IRegistrationService> _mockRegistrationService;
    private readonly Mock<ILogger<ParticipantController>> _mockLogger;

    private readonly ParticipantController _participantController;


    public ParticipantControllerTests()
    {
        _mockEventService = new Mock<IEventService>();
        _mockRegistrationService = new Mock<IRegistrationService>();
        _mockCheckService = new Mock<ICheckService>();
        _mockLogger = new Mock<ILogger<ParticipantController>>();

        _participantController = new ParticipantController(
         _mockEventService.Object,
         _mockRegistrationService.Object,
         _mockCheckService.Object,
         _mockLogger.Object
        );
    }

    [Fact]
    public async Task Index_GET_RedirectToAction_When_UserNotExists()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        _mockCheckService.Setup(s => s.CheckUserAsync(claimsPrincipal))
        .ReturnsAsync((IdentityUser)null);

        var result = await _participantController.Index();

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(AccountController.LoginRegister), redirectResult.ActionName);
        Assert.Equal("Account", redirectResult.ControllerName);
    }


    [Fact]
    public async Task Index_GET_ReturnViewForRole()
    {
        var identityUser = new IdentityUser
        {
            UserName = "test",
            Email = "test"
        };

        var events = new List<BaseEventViewModel>
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
        IsRegistered = true
            }
        };

        var eventIdsByUser = new List<int> { 1 };

        _mockCheckService.Setup(s => s.CheckUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(identityUser);
        _mockRegistrationService.Setup(s => s.GetEventIdsByUserIdAsync(identityUser.Id)).ReturnsAsync(eventIdsByUser);
        _mockEventService.Setup(s => s.GetEventsForParticipantAsync(eventIdsByUser)).ReturnsAsync(events);

        var result = await _participantController.Index();
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);

        var model = Assert.IsAssignableFrom<List<BaseEventViewModel>>(viewResult.Model);
        Assert.Equal(events.Count, model.Count);

    }

    [Fact]
    public async Task Participant_Register_GET_EventNotExists()
    {
        _mockCheckService.Setup(s => s.CheckEventAsync(It.IsAny<int>())).ReturnsAsync((Event)null);

        var result = await _participantController.Register(7);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Participant_Register_GET_EventExists()
    {
        var @event = new Event
        {
            Id = 7,
            Name = "Test",
            Description = "Test",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = false,
            CreatorId = "test"
        };

        _mockCheckService.Setup(s => s.CheckEventAsync(@event.Id)).ReturnsAsync(@event);

        var result = await _participantController.Register(@event.Id);
        var viewResult = Assert.IsType<ViewResult>(result);

        var model = Assert.IsType<Registration>(viewResult.Model);
        Assert.Equal(@event.Id, model.EventId);
    }


    [Fact]
    public async Task Register_POST_Registers_User_For_Event_When_ModelIsValid()
    {

        var @event = new Event
        {
            Id = 7,
            Name = "Test",
            Description = "Test",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = false,
            CreatorId = "test"
        };

        var model = new Registration
        {
            Id = 0,
            Name = "Test1",
            PhoneNumber = "1111",
            Email = "eeee",
            EventId = @event.Id
        };

        var identityUser = new IdentityUser
        {
            UserName = "test",
            Email = "test"
        };

        _mockCheckService.Setup(s => s.CheckEventAsync(@event.Id)).ReturnsAsync(@event);
        _mockCheckService.Setup(s => s.CheckUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(identityUser);
        _mockRegistrationService.Setup(s => s.RegisterUserAsync(model, identityUser.Id)).Returns(Task.CompletedTask);

        var result = await _participantController.Register(model);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }


    [Fact]
    public async Task Register_POST_Return_ViewResult_When_ModelIsInvalid()
    {
        var model = new Registration
        {
            Id = 0,
            Name = "Test1",
            PhoneNumber = "1111",
            Email = "eeee"
        };
        _participantController.ModelState.AddModelError("EventId", "Event id is missing");


        var result = await _participantController.Register(model);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(model, viewResult.Model);
    }


    [Fact]
    public async Task Register_POST_Redirects_NotFound()
    {
        var model = new Registration
        {
            Id = 0,
            Name = "Test1",
            PhoneNumber = "1111",
            Email = "eeee",
            EventId = 1
        };

        var identityUser = new IdentityUser
        {
            UserName = "test",
            Email = "test"
        };

        _mockCheckService.Setup(s => s.CheckEventAsync(1)).ReturnsAsync((Event)null);

        var result = await _participantController.Register(model);

        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task Register_POST_RedirectsToAction()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        var @event = new Event
        {
            Id = 7,
            Name = "Test",
            Description = "Test",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = false,
            CreatorId = "test"
        };

        var model = new Registration
        {
            Id = 0,
            Name = "Test1",
            PhoneNumber = "1111",
            Email = "eeee",
            EventId = @event.Id
        };


        _mockCheckService.Setup(s => s.CheckEventAsync(@event.Id)).ReturnsAsync(@event);
        _mockCheckService.Setup(s => s.CheckUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((IdentityUser)null);

        var result = await _participantController.Register(model);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(AccountController.LoginRegister), redirectResult.ActionName);
        Assert.Equal("Account", redirectResult.ControllerName);
    }
}