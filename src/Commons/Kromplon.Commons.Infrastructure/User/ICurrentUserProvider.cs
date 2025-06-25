using Microsoft.AspNetCore.Http;

namespace Kromplon.Commons.Infrastructure.User;

public interface ICurrentUserProvider
{
    Guid? UserId { get; }
    string? Email { get; }
}

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    
    public Guid? UserId  => _contextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value is { } userIdString && Guid.TryParse(userIdString, out var userId) ? userId : null;
    public string? Email => _contextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
}