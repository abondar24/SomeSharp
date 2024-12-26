using System.Security.Claims;
using EventRegistration.Models;
using Microsoft.AspNetCore.Identity;

namespace EventRegistration.Services;

public interface ICheckService
{
    public Task<Event?> CheckEventAsync(int id);
    public Task<IdentityUser?> CheckUserAsync(ClaimsPrincipal usr);

}