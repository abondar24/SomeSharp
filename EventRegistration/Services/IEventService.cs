using EventRegistration.Models;
using Microsoft.AspNetCore.Identity;

namespace EventRegistration.Services;

public interface IEventService
{
    public Task CreateEventAsync(Event model);
    public Task UpdateEventAsync(Event model);
    public Task ChangeEventStatusAsync(bool isDrafted, Event model);
    public Task<Event?> GetEventByIdAsync(int id);
    public Task<Event?> GetEventWithDetailsByIdAsync(int id);
    public Task<IList<EventViewModel>> GetUserEventsAsync(IdentityUser user, IList<string> roles, IList<int> eventIdsByUser);
    public Task<IList<BaseEventViewModel>> GetEventsForParticipantAsync(IList<int> eventIdsByUser);
    public Task DeleteEventAsync(Event model);
}