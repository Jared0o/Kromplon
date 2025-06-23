using Kromplon.Modules.Users.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kromplon.Modules.Users.Infrastructure;

public class UserContext : IdentityDbContext<User, Role, Guid>
{
    public UserContext(DbContextOptions options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Users");
        base.OnModelCreating(builder);
    }
}