using EventRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EventRegistration.Controllers;

public class AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger) : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    private readonly ILogger<AccountController> _logger = logger;

    // GET: Account/Login
    [HttpGet]
    public IActionResult Login()
    {

        return View();
    }

    // GET: /Account/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

 // GET: /Account/LoginRegister
    [HttpGet]
    public IActionResult LoginRegister()
    {
        return View();
    }




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

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
        

            var user = await _userManager.FindByEmailAsync(model.Email);
              if (user == null)
        {
            // Handle the case where user is not found
            ModelState.AddModelError(string.Empty, "Unexpected error occurred. User not found.");
            return View(model);
        }
            
            var roles = await _userManager.GetRolesAsync(user);
            
           if (roles.Contains("EventCreator") ||roles.Contains("EventParticipant") )
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
        await _signInManager.SignOutAsync();
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

        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
              await _userManager.AddToRoleAsync(user, model.Role); 
            await _signInManager.SignInAsync(user, isPersistent: false);

            _logger.LogInformation("User registered");
            return RedirectToAction("Index", "Home"); // Redirect to Home or specific page
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(model);
    }
}
