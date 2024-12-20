using EventRegistration.Data;
using EventRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Controllers;
public class HomeController(UserManager<IdentityUser> userManager, ApplicationDbContext context, ILogger<AccountController> logger) : Controller
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;

    private readonly ILogger<AccountController> _logger = logger;

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            // Handle the case where the user is not found
            return RedirectToAction("LoginRegister", "Account");
        }


        var roles = await _userManager.GetRolesAsync(user);

        bool isEventCreator = roles.Contains("EventCreator");

        _logger.LogInformation("User roles: {Roles}", string.Join(", ", roles));

        var userRegistrations = await _context.Registrations
             .Where(r => r.UserId == user.Id)
        .Select(r => r.EventId)
        .ToListAsync();

 _logger.LogInformation("Number of user registrations retrieved: {UserRegistrationsCount}", userRegistrations.Count);

        var events = await (from e in _context.Events
                            where (isEventCreator && e.CreatorId == user.Id) ||
                           (!isEventCreator && !e.IsDrafted)
                            select new EventViewModel
                            {
                                Event = e,
                                IsRegistered = userRegistrations.Contains(e.Id),
                                IsDrafted = e.IsDrafted,
                                Registrations = (from r in _context.Registrations
                                                 where r.EventId == e.Id
                                                 select new RegistrationViewModel
                                                 {
                                                     Name = r.Name,
                                                     Email = r.Email
                                                 }).ToList()
                            }).ToListAsync();

        _logger.LogInformation("Number of events retrieved: {EventCount}", events.Count);
        ViewData["Roles"] = roles;

        return View(events);
    }


}
