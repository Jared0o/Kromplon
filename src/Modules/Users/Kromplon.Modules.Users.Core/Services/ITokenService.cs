using Kromplon.Modules.Users.Core.Models;

namespace Kromplon.Modules.Users.Core.Services;

public interface ITokenService
{
    public Task<string> GenerateJwtToken(User user);
}