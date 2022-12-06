using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Identity.Entities;
using Domain.Identity.Enums;

namespace SystemOfWidget.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            foreach (var roleName in RoleName.GetList())
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new ApplicationRole() { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
        }
    }
}
