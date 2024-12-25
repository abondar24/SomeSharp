using System.Security.Claims;
using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistration.Controllers;

[Authorize(Roles = "EventParticipant")]
public class ParticipantController(EventService eventService, RegistrationService registrationService, CheckService checkService, ILogger<ParticipantController> logger) : Controller
{
    private readonly EventService _eventService = eventService;

    private readonly RegistrationService _registrationService = registrationService;

    private readonly CheckService _checkService = checkService;

    private readonly ILogger<ParticipantController> _logger = logger;

    // GET: Participant/Index
    public async Task<IActionResult> Index()
    {
        var user = await _checkService.CheckUserAsync(User);
        if (user == null)
        {
            return RedirectToAction(nameof(AccountController.LoginRegister), "Account");
        }

        var eventIdsByUser = await _registrationService.GetEventIdsByUserIdAsync(user.Id);
        var events = await _eventService.GetEventsForParticipantAsync(eventIdsByUser);

        return View(events);
    }

    // GET: Participant/Register/5
    public async Task<IActionResult> Register(int id)
    {
        var @event = await _checkService.CheckEventAsync(id);
        if (@event == null)
        {
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

            var @event = await _checkService.CheckEventAsync(model.EventId);
            if (@event == null)
            {
                return NotFound();
            }

            var user = await _checkService.CheckUserAsync(User);
            if (user == null)
            {
                return RedirectToAction(nameof(AccountController.LoginRegister), "Account");
            }

            await _registrationService.RegisterUserAsync(model, user.Id);
            return RedirectToAction("Index", "Home");
        }
        return View(model);
    }
}
