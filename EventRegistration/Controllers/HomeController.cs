using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

 private readonly ILogger<AccountController> _logger;

    public HomeController(UserManager<IdentityUser> userManager, ApplicationDbContext context,ILogger<AccountController> logger)
    {
        _userManager = userManager;
         _context = context;
          _logger = logger;
    }

    public async Task<IActionResult> Index() {
       var user = await _userManager.GetUserAsync(User);
       if (user == null)  {
        // Handle the case where the user is not found
        return RedirectToAction("LoginRegister", "Account");
      } 


  var roles = await _userManager.GetRolesAsync(user);
  _logger.LogInformation("User roles: {Roles}", string.Join(", ", roles));

 var userRegistrations = await _context.Registrations
        .Where(r => r.UserId == user.Id)
        .Select(r => r.EventId)
        .ToListAsync();

 _logger.LogInformation("Number of user registrations retrieved: {UserRegistrationsCount}", userRegistrations.Count);
  
     var events = await _context.Events
        .Select(e => new EventViewModel
        {
            Event = e,
            IsRegistered = userRegistrations.Contains(e.Id),
            Registrations = _context.Registrations
                .Where(r => r.EventId == e.Id)
                .Select(r => new RegistrationViewModel
                {
                    Name = r.Name,
                    Email = r.Email
                })
                .ToList()
        })
        .ToListAsync();

       _logger.LogInformation("Number of events retrieved: {EventCount}", events.Count);
       ViewData["Roles"] = roles;

    return View(events);
    }


}
