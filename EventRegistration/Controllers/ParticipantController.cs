using EventRegistration.Data;
using EventRegistration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Controllers;

[Authorize(Roles = "EventParticipant")]
public class ParticipantController(ApplicationDbContext context, ILogger<AccountController> logger, UserManager<IdentityUser> userManager) : Controller
{
    private readonly ApplicationDbContext _context = context;

    private readonly ILogger<AccountController> _logger = logger;

    private readonly UserManager<IdentityUser> _userManager = userManager;

    // GET: Participant/Index
    public async Task<IActionResult> Index()
    {
         var user = await _userManager.GetUserAsync(User);
        var userRegistrations = await _context.Registrations
            .Where(r => r.UserId == user.Id) // Ensure UserId is in Registration model
            .Select(r => r.EventId)
            .ToListAsync();

        var events = await _context.Events
            .Select(e => new
            {
                Event = e,
                IsRegistered = userRegistrations.Contains(e.Id)
            })
            .ToListAsync();

        return View(events);
    }

    // GET: Participant/Register/5
    public async Task<IActionResult> Register(int id)
    {
        var @event = await _context.Events.FindAsync(id);
        if (@event == null)
        {
            return NotFound();
        }
        return View(new Registration { EventId = id });
    }

    // POST: Participant/Register/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([Bind("Name,PhoneNumber,Email,EventId")] Registration registration)
    {
        ModelState.Remove("UserId");
        if (ModelState.IsValid)
        {

            var @event = await _context.Events.FindAsync(registration.EventId);
            if (@event == null)
            {
                 _logger.LogError("Event not found.");
                ModelState.AddModelError("", "Event not found.");
                return View(registration);
            }

            var user = await _userManager.GetUserAsync(User);

            _logger.LogInformation("HUI BLYAT {userId}",user.Id);
            registration.UserId = user.Id;

            _context.Add(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        } else {
             foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            _logger.LogError(error.ErrorMessage);
        }
        }
        return View(registration);
    }
}
