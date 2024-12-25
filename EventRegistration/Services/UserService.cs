using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace EventRegistration.Services;

public class UserService(UserManager<IdentityUser> userManager)
{
    private readonly UserManager<IdentityUser> _userManager = userManager;

    public async Task<IdentityUser> GetUserAsync(ClaimsPrincipal user)
    {
        return await _userManager.GetUserAsync(user);
    }

    public async Task<IList<string>> GetRolesByUserAsync(IdentityUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }


}