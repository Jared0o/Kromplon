using Microsoft.AspNetCore.Identity;

namespace Kromplon.Modules.Users.Core.Models;

public class User : IdentityUser<Guid>
{
    public override required string Email { get; set; }
}