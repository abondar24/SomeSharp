using EventRegistration.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistration.Controllers;
public class HomeController(UserService userService, EventService eventService,
RegistrationService registrationService, CheckService checkService, ILogger<HomeController> logger) : Controller
{
    private readonly UserService _userService = userService;

    private readonly EventService _eventService = eventService;

    private readonly RegistrationService _registrationService = registrationService;

    private readonly CheckService _checkService = checkService;

    private readonly ILogger<HomeController> _logger = logger;

    public async Task<IActionResult> Index()
    {
        var user = await _checkService.CheckUserAsync(User);
        if (user == null)
        {
            return RedirectToAction(nameof(AccountController.LoginRegister), "Account");
        }


        var roles = await _userService.GetRolesByUserAsync(user);
        ViewData["Roles"] = roles;

        var eventIdsByUserId = await _registrationService.GetEventIdsByUserIdAsync(user.Id);

        var events = await _eventService.GetUserEventsAsync(user, roles, eventIdsByUserId);


        return View(events);
    }


}
