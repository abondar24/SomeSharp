using EventRegistration.Data;
using EventRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Services;

public class EventService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

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
}