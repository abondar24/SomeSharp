using System.Security.Claims;
using EventRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventRegistration.Services;

public class CheckService(IUserService userService, IEventService eventService, ILogger<CheckService> logger) : ICheckService
{
    private readonly IUserService _userService = userService;

    private readonly IEventService _eventService = eventService;

    private readonly ILogger<CheckService> _logger = logger;

    public async Task<Event?> CheckEventAsync(int id)
    {
        if (id <= 0)
        {
            _logger.LogError("Event not found by id {}", id);
            return null;
        }

        var @event = await _eventService.GetEventByIdAsync(id);
        if (@event == null)
        {
            _logger.LogError("Event not found by id {}", id);
            return null;
        }

        return @event;
    }

    public async Task<IdentityUser?> CheckUserAsync(ClaimsPrincipal usr)
    {
        var user = await _userService.GetUserAsync(usr);
        if (user == null)
        {
            _logger.LogError("User not found for the current request.");
            return null;
        }

        return user;
    }
}