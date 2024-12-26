using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Mvc;


namespace EventRegistration.Controllers;

public class AccountController(IUserService userService, ILogger<AccountController> logger) : Controller
{

    private readonly IUserService _userService = userService;


    private readonly ILogger<AccountController> _logger = logger;

    // GET: Account/Login
    [HttpGet]
    public IActionResult Login() => View();

    // GET: /Account/Register
    [HttpGet]
    public IActionResult Register() => View();

    // GET: /Account/LoginRegister
    [HttpGet]
    public IActionResult LoginRegister() => View();

    // POST: Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {

        if (ModelState.IsValid)
        {
            var result = await _userService.PasswordSignInAsync(model);
            if (result.Succeeded)
            {

                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    _logger.LogError("User not found by email");
                    return RedirectToAction(nameof(AccountController.LoginRegister), "Account");
                }

                var roles = await _userService.GetRolesByUserAsync(user);
                if (roles.Contains("EventCreator") || roles.Contains("EventParticipant"))
                {
                    return RedirectToAction("Index", "Home");
                }

            }
        }
        return View(model);
    }

    // POST: Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _userService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


      // POST: Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var isUserCreated = await _userService.CreateUserAsync(model);
            if (isUserCreated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                _logger.LogError("Registration of new user failed");
            }
        }
        return View(model);
    }
}
