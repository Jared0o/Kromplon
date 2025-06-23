using FluentValidation;
using Kromplon.Commons.Abstractions.Result;
using Kromplon.Modules.Users.Core.Models;
using Kromplon.Modules.Users.Core.Models.Enums;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace Kromplon.Modules.Users.Core.Commands.AddUser;

public class AddUserCommandHandler(UserManager<User> userManager, IValidator<AddUserCommand> validator)
    : ICommandHandler<AddUserCommand, Result>
{
    public async ValueTask<Result> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Failure(ErrorType.ValidationError, string.Join("\n", validatorResult.Errors.Select(e => e.ErrorMessage)));
        }

        var user = await userManager.FindByEmailAsync(command.RegisterUserDto.Email);
        if (user != null)
        {
            return Result.Failure(ErrorType.ValidationError, "User with this email already exists.");
        }
        
        user = new()
        {
            Id = Guid.CreateVersion7(),
            UserName = command.RegisterUserDto.Email,
            Email = command.RegisterUserDto.Email
        };

        var result = await userManager.CreateAsync(user, command.RegisterUserDto.Password);
        if (!result.Succeeded)
        {
            return Result.Failure(ErrorType.ValidationError, string.Join("\n", result.Errors.Select(e => e.Description)));
        }

        await userManager.AddToRoleAsync(user, nameof(Roles.User));
        
        return Result.Success();
    }
}