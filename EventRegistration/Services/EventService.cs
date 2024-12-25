using EventRegistration.Data;
using EventRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Services;

public class EventService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;



    public async Task CreateEventAsync(Event model)
    {
        _context.Events.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEventAsync(Event model)
    {
        _context.Events.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task<Event> GetEventByIdAsync(int id) => await _context.Events.FindAsync(id);

    public async Task ChangeEventStatusAsync(bool isDrafted, Event model)
    {
        model.IsDrafted = isDrafted;
        _context.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task<Event> GetEventWithDetailsByIdAsync(int id) => await _context.Events.Include(ev => ev.Registrations)
          .FirstOrDefaultAsync(ev => ev.Id == id);


    public async Task<IList<EventViewModel>> GetUserEventsAsync(IdentityUser user, IList<string> roles, IList<int> eventIdsByUser)
    {
        bool isEventCreator = roles.Contains("EventCreator");

        return await (from e in _context.Events
                      where (isEventCreator && e.CreatorId == user.Id) ||
                     (!isEventCreator && !e.IsDrafted)
                      select new EventViewModel
                      {
                          Event = e,
                          IsRegistered = eventIdsByUser.Contains(e.Id),
                          IsDrafted = e.IsDrafted,
                          Registrations = (from r in _context.Registrations
                                           where r.EventId == e.Id
                                           select new RegistrationViewModel
                                           {
                                               Name = r.Name,
                                               Email = r.Email
                                           }).ToList()
                      }).ToListAsync();
    }

    public async Task<IList<EventParticipantViewModel>> GetEventsForParticipantAsync(IList<int> eventIdsByUser) => await _context.Events
            .Select(e => new EventParticipantViewModel
            {
                Event = e,
                IsRegistered = eventIdsByUser.Contains(e.Id)
            })
            .ToListAsync();

    public async Task DeleteEventAsync(Event model)
    {
        _context.Events.Remove(model);
        await _context.SaveChangesAsync();
    }
}