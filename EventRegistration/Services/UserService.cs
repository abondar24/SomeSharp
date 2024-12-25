using System.Security.Claims;
using EventRegistration.Models;
using Microsoft.AspNetCore.Identity;

namespace EventRegistration.Services;

public class UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
{
    private readonly UserManager<IdentityUser> _userManager = userManager;

    private readonly SignInManager<IdentityUser> _signInManager = signInManager;

    public async Task<bool> CreateUserAsync(RegisterViewModel model)
    {
        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, model.Role);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return true;
        }
        return false;
    }

    public async Task<IdentityUser> GetUserAsync(ClaimsPrincipal user) => await _userManager.GetUserAsync(user);
    public async Task<IdentityUser> GetUserByEmailAsync(string Email) => await _userManager.FindByEmailAsync(Email);
    public async Task<IList<string>> GetRolesByUserAsync(IdentityUser user) => await _userManager.GetRolesAsync(user);
    public async Task<SignInResult> PasswordSignInAsync(LoginViewModel model) => await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
    public async Task SignOutAsync() => await _signInManager.SignOutAsync();

}