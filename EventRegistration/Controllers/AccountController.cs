using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EventRegistration.Controllers;

public class AccountController(UserService userService, ILogger<AccountController> logger) : Controller
{

    private readonly UserService _userService = userService;


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

        if (!ModelState.IsValid)
        {
              foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            _logger.LogError(error.ErrorMessage);
        }
            return View(model);
        }

        var result = await _userService.PasswordSignInAsync(model);
        if (result.Succeeded)
        {

            var user = await _userService.GetUserByEmailAsync(model.Email);
            if (user == null)
            {
                // Handle the case where user is not found
                ModelState.AddModelError(string.Empty, "Unexpected error occurred. User not found.");
                return View(model);
            }

            var roles = await _userService.GetRolesByUserAsync(user);
            if (roles.Contains("EventCreator") || roles.Contains("EventParticipant"))
            {
          
                return RedirectToAction("Index", "Home"); // Redirect to Event creation page for EventCreators
            }
         
        }
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
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
        if (!ModelState.IsValid)
        {
              foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            _logger.LogError(error.ErrorMessage);
        }
            return View(model);
        }

        var isUserCreated = await _userService.CreateUserAsync(model);
        if (isUserCreated)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        else
        {
            _logger.LogError("Registration of new user failed");
        }

        return View(model);
    }
}
