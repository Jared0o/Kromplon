using Kromplon.Commons.Infrastructure.Api;
using Kromplon.Commons.Infrastructure.User;
using Kromplon.Modules.Users.Core.Commands.AddUser;
using Kromplon.Modules.Users.Core.Commands.SingIn;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Kromplon.Modules.Users.Api.Routes;

public static class UserEndpoints
{
    internal static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async context => await context.Response.WriteAsync("It's user module in Kromplon api."));
        group.MapPost("/register", CreateUser);
        group.MapPost("/signin", SignInUser);
        group.MapGet("/me", GetCurrentUser).RequireAuthorization(AuthorizationPolicies.User);

        return group;
    }

    private static async Task<IResult> CreateUser([FromBody] RegisterUserDto userDto, [FromServices] ISender sender)
    {
        var res = await sender.Send(new AddUserCommand(userDto));
        return res.IsSuccess ? Results.NoContent() : ApiResult.HandleError(res.Error);
    }
    
    private static async Task<IResult> SignInUser([FromBody] SignInDto signInDto, [FromServices] ISender sender)
    {
        var res = await sender.Send(new SignInCommand(signInDto));
        return res.IsSuccess ? Results.Ok(res.Value) : ApiResult.HandleError(res.Error);
    }
    
    private static async Task<IResult> GetCurrentUser([FromServices] ICurrentUserProvider currentUserProvider)
    {
        var userGuid = currentUserProvider.UserId;
        return userGuid != null ? Results.Ok(userGuid) : Results.Unauthorized();
    }
}