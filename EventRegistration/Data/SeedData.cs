using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EventRegistration.Data;
public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "EventCreator", "EventParticipant" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create default EventCreator user
        IdentityUser user = await userManager.FindByEmailAsync("creator@example.com");
        if (user == null)
        {
            user = new IdentityUser()
            {
                UserName = "creator@example.com",
                Email = "creator@example.com",
            };
            await userManager.CreateAsync(user, "Password123!");
        }
        await userManager.AddToRoleAsync(user, "EventCreator");

        // Create default EventParticipant user
        user = await userManager.FindByEmailAsync("participant@example.com");
        if (user == null)
        {
            user = new IdentityUser()
            {
                UserName = "participant@example.com",
                Email = "participant@example.com",
            };
            await userManager.CreateAsync(user, "Password123!");
        }
        await userManager.AddToRoleAsync(user, "EventParticipant");
    }
}
