using EventRegistration.Models;
using Microsoft.AspNetCore.Identity;

namespace EventRegistration.Services;

public class SignInService(SignInManager<IdentityUser> signInManager)
{
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    public async Task<SignInResult> PasswordSignInAsync(LoginViewModel model) => await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
    public async Task SignOutAsync() => await _signInManager.SignOutAsync();
}