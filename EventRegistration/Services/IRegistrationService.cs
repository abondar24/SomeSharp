using EventRegistration.Models;

namespace EventRegistration.Services;

public interface IRegistrationService
{
    public Task RegisterUserAsync(Registration registration, string userId);
    public Task<IList<int>> GetEventIdsByUserIdAsync(string userId);
    public Task<IList<Registration>> GetRegistrationsByEventIdAsync(int eventId);
}