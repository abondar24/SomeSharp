using System.Security.Claims;
using EventRegistration.Controllers;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EventRegistration.Tests.Controllers;

public class EventControllerTests
{

    private readonly Mock<IEventService> _mockEventService;
    private readonly Mock<ICheckService> _mockCheckService;
    private readonly Mock<IRegistrationService> _mockRegistrationService;
    private readonly Mock<ILogger<EventController>> _mockLogger;

    private readonly EventController _eventController;

    public EventControllerTests()
    {
        _mockEventService = new Mock<IEventService>();
        _mockRegistrationService = new Mock<IRegistrationService>();
        _mockCheckService = new Mock<ICheckService>();

        _mockLogger = new Mock<ILogger<EventController>>();

        _eventController = new EventController(_mockEventService.Object, _mockRegistrationService.Object,
        _mockCheckService.Object, _mockLogger.Object);
    }

    [Fact]
    public void Create_GET_Returns_ViewResult()
    {
        var result = _eventController.Create();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Create_POST_Creates_Event_When_ModelIsValid()
    {
        var model = new Event
        {
            Id = 0,
            Name = "Test",
            Description = "Test",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = false,
            CreatorId = "test"
        };

        var identityUser = new IdentityUser
        {
            UserName = "test",
            Email = "test"
        };

        _mockCheckService.Setup(s => s.CheckUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(identityUser);
        _mockEventService.Setup(s => s.CreateEventAsync(model)).Returns(Task.CompletedTask);

        var result = await _eventController.Create(model);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }


    [Fact]
    public async Task Create_POST_Return_ViewResult_When_ModelIsInvalid()
    {
        var model = new Event
        {
            Id = 0,
            Name = "Test",
            Description = "Test",
            Location = "Test",
        };
        _eventController.ModelState.AddModelError("StartDate", "Start date is missing");


        var result = await _eventController.Create(model);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(model, viewResult.Model);
    }

    [Fact]
    public async Task Edit_GET_Returns_NotFound_When_EventNotExists()
    {
        _mockCheckService.Setup(s => s.CheckEventAsync(It.IsAny<int>())).ReturnsAsync((Event)null);

        var result = await _eventController.Edit(7);
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task Edit_GET_Returns_VoewResult_When_EventExists()
    {
        var model = new Event
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

        _mockCheckService.Setup(s => s.CheckEventAsync(1)).ReturnsAsync(model);

        var result = await _eventController.Edit(model.Id);
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Equal(model, viewResult.Model);
    }


    [Fact]
    public async Task Edit_POST_Updates_Event_When_ModelIsValid()
    {
        var model = new Event
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

        _mockEventService.Setup(s => s.UpdateEventAsync(model)).Returns(Task.CompletedTask);

        var result = await _eventController.Edit(model);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }

    [Fact]
    public async Task Edit_POST_Return_ViewResult_When_ModelIsInvalid()
    {
        var model = new Event
        {
            Id = 0,
            Name = "Test",
            Description = "Test",
            Location = "Test",
        };
        _eventController.ModelState.AddModelError("StartDate", "Start date is missing");


        var result = await _eventController.Edit(model);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(model, viewResult.Model);
    }


    [Fact]
    public async Task Registrations_GET_Returns_NotFound_When_EventNotExists()
    {
        _mockCheckService.Setup(s => s.CheckEventAsync(It.IsAny<int>())).ReturnsAsync((Event)null);

        var result = await _eventController.Registrations(7);
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task Registrations_GET_Returns_ViewResult_When_EventExists()
    {
        var model = new Event
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
        var registrations = new List<Registration>
        {
            new() {
            Id = 0,
            Name = "Test1",
            PhoneNumber = "1111",
            Email = "eeee",
            EventId = 1
           }
        };

        _mockCheckService.Setup(s => s.CheckEventAsync(model.Id)).ReturnsAsync(model);
        _mockRegistrationService.Setup(s => s.GetRegistrationsByEventIdAsync(model.Id)).ReturnsAsync(registrations);

        var result = await _eventController.Registrations(model.Id);
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Equal(registrations, viewResult.Model);
    }


    [Fact]
    public async Task Details_GET_Returns_NotFound_When_EventNotExists()
    {
        _mockCheckService.Setup(s => s.CheckEventAsync(It.IsAny<int>())).ReturnsAsync((Event)null);

        var result = await _eventController.Details(7);
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task Details_GET_Returns_ViewResult_When_EventExists()
    {
        var model = new Event
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

        _mockEventService.Setup(s => s.GetEventWithDetailsByIdAsync(model.Id)).ReturnsAsync(model);


        var result = await _eventController.Details(model.Id);
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Equal(model, viewResult.Model);
    }


    [Fact]
    public async Task ChangeStatus_GET_Returns_NotFound_When_EventNotExists()
    {
        _mockCheckService.Setup(s => s.CheckEventAsync(It.IsAny<int>())).ReturnsAsync((Event)null);

        var result = await _eventController.ChangeStatus(7, false);
        Assert.IsType<NotFoundResult>(result);
    }



    [Fact]
    public async Task ChangeStatus_GET_Returns_ViewResult_When_EventExists()
    {
        var model = new Event
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

        _mockCheckService.Setup(s => s.CheckEventAsync(model.Id)).ReturnsAsync(model);
        _mockEventService.Setup(s => s.ChangeEventStatusAsync(true, model)).Returns(Task.CompletedTask);


        var result = await _eventController.ChangeStatus(model.Id, true);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }

    [Fact]
    public async Task Delete_POST_Returns_NotFound_When_EventNotExists()
    {
        _mockCheckService.Setup(s => s.CheckEventAsync(It.IsAny<int>())).ReturnsAsync((Event)null);

        var result = await _eventController.DeleteEvent(1);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_POST_Deletes_Event_When_EventExists()
    {
        var model = new Event
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

        _mockCheckService.Setup(s => s.CheckEventAsync(model.Id)).ReturnsAsync(model);
        _mockEventService.Setup(s => s.DeleteEventAsync(model)).Returns(Task.CompletedTask);


        var result = await _eventController.ChangeStatus(model.Id, true);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }

}