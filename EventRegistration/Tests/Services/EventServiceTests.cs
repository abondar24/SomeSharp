using EventRegistration.Data;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventRegistration.Tests.Services;

public class EventServiceTests
{
    private readonly IEventService _eventService;

    private readonly ApplicationDbContext _context;

    public EventServiceTests()
    {

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _eventService = new EventService(_context);
    }


    [Fact]
    public async Task CreateEventAsync_ShoudAddEvent()
    {
        var @event = new Event
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

        await _eventService.CreateEventAsync(@event);

        var result = await _context.Events.FindAsync(1);
        Assert.NotNull(result);
        Assert.Equal(@event.Name, result?.Name);
    }

    [Fact]
    public async Task UpdateEventAsync_ShoudUpdateEventDescription()
    {
        var @event = new Event
        {
            Id = 0,
            Name = "Test1",
            Description = "",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = true,
            CreatorId = "test"

        };

        await _eventService.CreateEventAsync(@event);

        @event.Description = "Test";
        await _eventService.UpdateEventAsync(@event);

        var result = await _context.Events.FindAsync(1);
        Assert.NotNull(result);
        Assert.Equal(@event.Name, result?.Name);
        Assert.Equal(@event.Description, result?.Description);
    }

    [Fact]
    public async Task ChangeEventStatusAsync_ShouldUpdateEventStatus()
    {
        var @event = new Event
        {
            Id = 0,
            Name = "Test1",
            Description = "",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = true,
            CreatorId = "test"

        };
        await _eventService.CreateEventAsync(@event);

        await _eventService.ChangeEventStatusAsync(false, @event);

        var result = await _context.Events.FindAsync(1);
        Assert.NotNull(result);
        Assert.False(result?.IsDrafted);

    }

    [Fact]
    public async Task GetEventByIdAsync_ShouldReturnAnEvent()
    {
        var @event = new Event
        {
            Id = 0,
            Name = "Test1",
            Description = "",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = true,
            CreatorId = "test"

        };
        await _eventService.CreateEventAsync(@event);
        Assert.Equal(1, @event.Id);

        var result = await _eventService.GetEventByIdAsync(@event.Id);
        Assert.NotNull(result);
        Assert.Equal(@event.Name, result?.Name);
    }

    [Fact]
    public async Task GetEventWithDetailsByIdAsync_ShouldReturnEventWithRegistration()
    {
        var @event = new Event
        {
            Id = 0,
            Name = "Test1",
            Description = "",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = true,
            CreatorId = "test"

        };
        await _eventService.CreateEventAsync(@event);

        var registration = new Registration
        {
            Id = 0,
            Name = "Test1",
            PhoneNumber = "1111",
            Email = "eeee",
            EventId = @event.Id,
            UserId = @event.CreatorId
        };

        _context.Add(registration);
        await _context.SaveChangesAsync();

        var result = await _eventService.GetEventWithDetailsByIdAsync(@event.Id);
        Assert.NotNull(result);
        Assert.Single(result.Registrations);
    }

    [Fact]
    public async Task GetUserEventsAsync_ShouldReturnUserEvents()
    {
        var user = new IdentityUser { UserName = "test", Email = "test" };
        var roles = new List<string> { "EventCreator" };
        var eventIdsByUser = new List<int> { 1 };

        var @event = new Event
        {
            Id = 0,
            Name = "Test1",
            Description = "",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = true,
            CreatorId = user.Id

        };
        await _eventService.CreateEventAsync(@event);


        var registration = new Registration
        {
            Id = 0,
            Name = "Test1",
            PhoneNumber = "1111",
            Email = "eeee",
            EventId = @event.Id,
            UserId = @event.CreatorId
        };

        await _context.Registrations.AddAsync(registration);
        await _context.SaveChangesAsync();

        var result = await _eventService.GetUserEventsAsync(user, roles, eventIdsByUser);
        Assert.Single(result);

        Assert.Equal(@event.Id, result[0].Event.Id);
        Assert.True(result[0].IsRegistered);
        Assert.True(result[0].IsDrafted);
        Assert.Single(result[0].Registrations);
        Assert.Equal(registration.Name, result[0].Registrations[0].Name);
    }

    [Fact]
    public async Task GetEventsForParticipantAsync_ShouldReturnEvent()
    {
        var eventIdsByUser = new List<int> { 1 };

        var @event = new Event
        {
            Id = 0,
            Name = "Test1",
            Description = "",
            Location = "Test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            IsDrafted = true,
            CreatorId = "test"

        };
        await _eventService.CreateEventAsync(@event);

        var result = await _eventService.GetEventsForParticipantAsync(eventIdsByUser);
        Assert.Single(result);
        Assert.Equal(@event.Id, result[0].Event.Id);
        Assert.True(result[0].IsRegistered);
    }

    [Fact]
    public async Task DeleteEventAsync_ShoudDeleteEvent()
    {
        var @event = new Event
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

        await _eventService.CreateEventAsync(@event);

        await _eventService.DeleteEventAsync(@event);

        var result = await _context.Events.FindAsync(1);
        Assert.Null(result);

    }

}