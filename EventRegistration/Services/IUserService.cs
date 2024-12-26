using System.Security.Claims;
using EventRegistration.Models;
using Microsoft.AspNetCore.Identity;

namespace EventRegistration.Services;

public interface IUserService
{
    public Task<bool> CreateUserAsync(RegisterViewModel model);
    public Task<IdentityUser?> GetUserAsync(ClaimsPrincipal user);
    public Task<IdentityUser?> GetUserByEmailAsync(string Email);
    public Task<IList<string>> GetRolesByUserAsync(IdentityUser user);
    public Task<SignInResult> PasswordSignInAsync(LoginViewModel model);
    public Task SignOutAsync();
}