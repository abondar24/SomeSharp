using EventRegistration.Models;
using EventRegistration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistration.Controllers;

public class EventController(IEventService eventService, IRegistrationService registrationService,
ICheckService checkService, ILogger<EventController> logger) : Controller
{

    private readonly IEventService _eventService = eventService;

    private readonly IRegistrationService _registrationService = registrationService;

    private readonly ICheckService _checkService = checkService;

    private readonly ILogger<EventController> _logger = logger;


    // GET: Event/Create
    [Authorize(Roles = "EventCreator")]
    public IActionResult Create() => View();

    // POST: Event/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "EventCreator")]
    public async Task<IActionResult> Create(Event model)
    {
        ModelState.Remove("CreatorId");
        if (ModelState.IsValid)
        {

            var user = await _checkService.CheckUserAsync(User);
            if (user == null)
            {
                return RedirectToAction(nameof(AccountController.LoginRegister), "Account");
            }
            model.CreatorId = user.Id;

            await _eventService.CreateEventAsync(model);

            // Redirect to Home/Index after successful creation
            return RedirectToAction("Index", "Home");

        }
        return View(model);
    }


    // GET: Event/Edit
    [Authorize(Roles = "EventCreator")]
    public async Task<IActionResult> Edit(int id)
    {
        var @event = await _checkService.CheckEventAsync(id);
        if (@event == null)
        {
            _logger.LogError("Event not found by id {}", id);
            return NotFound();
        }


        return View(@event);
    }

    // POST: Event/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "EventCreator")]
    public async Task<IActionResult> Edit(Event model)
    {
        ModelState.Remove("CreatorId");
        if (ModelState.IsValid)
        {

            await _eventService.UpdateEventAsync(model);

            // Redirect to Home/Index after successful creation
            return RedirectToAction("Index", "Home");

        }
        return View(model);
    }

    // GET: Event/Registrations/5
    [Authorize(Roles = "EventCreator")]
    public async Task<IActionResult> Registrations(int id)
    {
        var @event = await _checkService.CheckEventAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        var registrations = await _registrationService.GetRegistrationsByEventIdAsync(id);

        ViewBag.EventName = @event.Name;
        return View(registrations);
    }


    // GET: Event/Details/5
    public async Task<IActionResult> Details(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var eventDetailed = await _eventService.GetEventWithDetailsByIdAsync(id);
        if (eventDetailed == null)
        {
            _logger.LogError("Event not found by id {}", id);
            return NotFound();
        }

        return View(eventDetailed);
    }


    //GET: Event/ChangeStatus/5?isDrafted=false 
    [HttpGet]
    [Authorize(Roles = "EventCreator")]
    public async Task<IActionResult> ChangeStatus(int id, bool isDrafted)
    {
        var @event = await _checkService.CheckEventAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        await _eventService.ChangeEventStatusAsync(isDrafted, @event);

        return RedirectToAction("Index", "Home");
    }


    //POST: Event/DeleteEvent/5 
    [HttpPost, ActionName("DeleteEvent")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "EventCreator")]
    public async Task<IActionResult> DeleteEvent(int id)
    {

        var @event = await _checkService.CheckEventAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        await _eventService.DeleteEventAsync(@event);

        return RedirectToAction("Index", "Home");
    }


}
