using Kromplon.Modules.Users.Core.Models;
using Kromplon.Modules.Users.Core.Services;
using Kromplon.Modules.Users.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kromplon.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddDbContext<UserContext>(opt =>
        {
            opt.UseNpgsql(config.GetConnectionString("UsersContext"));
        });

        services.AddIdentityCore<User>(x =>
            {
                x.Password.RequireDigit = true;
                x.Password.RequireLowercase = true;
                x.Password.RequireUppercase = true;
                x.Password.RequireNonAlphanumeric = true;
                x.Password.RequiredLength = 8;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<UserContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleManager<RoleManager<Role>>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}