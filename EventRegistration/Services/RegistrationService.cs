using EventRegistration.Data;
using EventRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Services;

public class RegistrationService(ApplicationDbContext context)
{

    private readonly ApplicationDbContext _context = context;

    public async Task<IList<int>> GetEventIdsByUserIdAsync(string userId)
    {
        return await _context.Registrations
             .Where(r => r.UserId == userId)
             .Select(r => r.EventId)
             .ToListAsync();
    }
}