using EventRegistration.Data;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventRegistration.Tests.Services;

public class RegistrationServiceTests
{
    private readonly IRegistrationService _registrationService;

    private readonly ApplicationDbContext _context;

    public RegistrationServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
         .Options;

        _context = new ApplicationDbContext(options);
        _registrationService = new RegistrationService(_context);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldRegisterUser()
    {
        var registration = new Registration
        {
            Id = 0,
            Name = "Test1",
            PhoneNumber = "1111",
            Email = "eeee",
            EventId = 1
        };
        var userId = "test";

        await _registrationService.RegisterUserAsync(registration, userId);

        var result = await _context.Registrations.FindAsync(1);
        Assert.NotNull(result);
        Assert.Equal(registration.Name, result?.Name);
        Assert.Equal(userId, result?.UserId);
    }


    [Fact]
    public async Task GetEventIdsByUserId_ShouldReturnEventForRegisteredUser()
    {
        var registration = new Registration
        {
            Id = 0,
            Name = "Test1",
            PhoneNumber = "1111",
            Email = "eeee",
            EventId = 1
        };
        var userId = "test";

        await _registrationService.RegisterUserAsync(registration, userId);

        var result = await _registrationService.GetEventIdsByUserIdAsync(userId);
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(1, result[0]);
    }


    [Fact]
    public async Task GetRegistrationsByEventIdAsync_ShouldReturnRegistrations()
    {
        var registration = new Registration
        {
            Id = 0,
            Name = "Test1",
            PhoneNumber = "1111",
            Email = "eeee",
            EventId = 1
        };
        var userId = "test";

        await _registrationService.RegisterUserAsync(registration, userId);

        var result = await _registrationService.GetRegistrationsByEventIdAsync(registration.EventId);
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(registration.Id, result[0].Id);
    }
}