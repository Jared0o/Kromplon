using Microsoft.AspNetCore.Authorization;

namespace Kromplon.Commons.Infrastructure.User;

public static class AuthorizationPolicies
{
    public const string User = "User";
    public const string Moderator = "Moderator";
    public const string Admin = "Admin";
    
    public static void AddAuthorizationPolicies(this AuthorizationOptions opt)
    {
        opt.AddPolicy(User, policy => policy.RequireRole("User"));
        opt.AddPolicy(Moderator, policy => policy.RequireRole("Moderator"));
        opt.AddPolicy(Admin, policy => policy.RequireRole("Admin"));
    }
}