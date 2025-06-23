using Kromplon.Commons.Infrastructure.Api;
using Kromplon.Modules.Users.Core.Commands.AddUser;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Kromplon.Modules.Users.Api.Routes;

public static class UserEndpoints
{
    internal static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/register", CreateUser);

        return group;
    }

    private static async Task<IResult> CreateUser([FromBody] RegisterUserDto userDto, [FromServices] ISender sender)
    {
        var res = await sender.Send(new AddUserCommand(userDto));
        return res.IsSuccess ? Results.NoContent() : ApiResult.HandleError(res.Error);
    }
}