using EventRegistration.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistration.Controllers;
public class HomeController(IUserService userService, IEventService eventService,
IRegistrationService registrationService, ICheckService checkService, ILogger<HomeController> logger) : Controller
{
    private readonly IUserService _userService = userService;

    private readonly IEventService _eventService = eventService;

    private readonly IRegistrationService _registrationService = registrationService;

    private readonly ICheckService _checkService = checkService;

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
