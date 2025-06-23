using Kromplon.Modules.Users.Core.Models;
using Kromplon.Modules.Users.Core.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kromplon.Modules.Users.Infrastructure.Helpers;

public static class ApplyUsersMigrations
{
    public static async Task ApplyUsersMigration(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>();
        await dbContext.Database.MigrateAsync();

        // Seed roles
        await SeedRoles(scope.ServiceProvider);
    }
    
    private static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        var roleNames = Enum.GetNames<Roles>();

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new Role { Name = roleName });
            }
        }
    } 
}