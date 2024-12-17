using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


public class EventController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventController(ApplicationDbContext context)
    {
        _context = context;
    }

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
        if (ModelState.IsValid)
        {
            _context.Events.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home"); // Redirect to Home/Index after successful creation
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


}
