using System.Security.Claims;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistration.Controllers;

[Authorize(Roles = "EventParticipant")]
public class ParticipantController(UserService userService, EventService eventService,
RegistrationService registrationService, ILogger<ParticipantController> logger) : Controller
{
    private readonly UserService _userService = userService;

    private readonly EventService _eventService = eventService;

    private readonly RegistrationService _registrationService = registrationService;

    private readonly ILogger<ParticipantController> _logger = logger;

    // GET: Participant/Index
    public async Task<IActionResult> Index()
    {
        var user = await CheckUserAsync(User);
        if (user == null)
        {
            _logger.LogError("User not found for the current request.");
            return RedirectToAction(nameof(AccountController.LoginRegister), "Account");
        }

        var eventIdsByUser = await _registrationService.GetEventIdsByUserIdAsync(user.Id);
        var events = await _eventService.GetEventsForParticipantAsync(eventIdsByUser);

        return View(events);
    }

    // GET: Participant/Register/5
    public async Task<IActionResult> Register(int id)
    {
        var @event = await CheckEventAsync(id);
        if (@event == null)
        {
            _logger.LogError("Event not found by id {}", id);
            return NotFound();
        }

        return View(new Registration { EventId = id });
    }

    // POST: Participant/Register/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([Bind("Name,PhoneNumber,Email,EventId")] Registration model)
    {
        ModelState.Remove("UserId");
        if (ModelState.IsValid)
        {

            var @event = await CheckEventAsync(model.EventId);
            if (@event == null)
            {
                _logger.LogError("Event not found by id {}", model.EventId);
                return NotFound();
            }

            var user = await CheckUserAsync(User);
            if (user == null)
            {
                _logger.LogError("User not found for the current request.");
                return RedirectToAction(nameof(AccountController.LoginRegister), "Account");
            }

            await _registrationService.RegisterUserAsync(model, user.Id);
            return RedirectToAction("Index", "Home");
        } else {
             foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            _logger.LogError(error.ErrorMessage);
        }
        }
        return View(model);
    }

    private async Task<Event?> CheckEventAsync(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        var @event = await _eventService.GetEventByIdAsync(id);
        if (@event == null)
        {
            return null;
        }

        return @event;
    }

    private async Task<IdentityUser?> CheckUserAsync(ClaimsPrincipal usr)
    {
        var user = await _userService.GetUserAsync(usr);
        if (user == null)
        {
            return null;
        }

        return user;
    }
}
