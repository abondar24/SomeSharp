using EventRegistration.Data;
using EventRegistration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Controllers;

public class EventController(ApplicationDbContext context, UserManager<IdentityUser> userManager) : Controller
{
    private readonly ApplicationDbContext _context = context;

    private readonly UserManager<IdentityUser> _userManager = userManager;

    // GET: Event/Create
    [Authorize(Roles = "EventCreator")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Event/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "EventCreator")]
    public async Task<IActionResult> Create(Event model)
    {
        ModelState.Remove("CreatorId");
        if (ModelState.IsValid)
        {

            var user = await _userManager.GetUserAsync(User);
            model.CreatorId = user.Id;

            _context.Events.Add(model);
            await _context.SaveChangesAsync();

            // Redirect to Home/Index after successful creation
            return RedirectToAction("Index", "Home");

        }
        return View(model);
    }

    // GET: Event/Registrations/5
    [Authorize(Roles = "EventCreator")]
    public async Task<IActionResult> Registrations(int id)
    {
        var @event = await _context.Events.FindAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        var registrations = await _context.Registrations
            .Where(r => r.EventId == id)
            .ToListAsync();

        ViewBag.EventName = @event.Name;
        return View(registrations);
    }


    // GET: Event/Details/5
    public async Task<IActionResult> Details(int id)
    {     
        if (id <=0){
            return NotFound();
        }

         var @event = await _context.Events.Include(ev => ev.Registrations)
         .FirstOrDefaultAsync(ev=> ev.Id == id);

         if (@event == null)
        {
            return NotFound();
        }
    
              return View(@event);
    }


    //GET: Event/ChangeStatus/5?isDrafted=false 
    [HttpGet]
    [Authorize(Roles = "EventCreator")]
    public async Task<IActionResult> ChangeStatus(int id, bool isDrafted)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var @event = await _context.Events.FindAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        @event.IsDrafted = isDrafted;
        _context.Update(@event);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }


    //POST: Event/DeleteEvent/5 
    [HttpPost, ActionName("DeleteEvent")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "EventCreator")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var @event = await _context.Events.FindAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        _context.Events.Remove(@event);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

}
