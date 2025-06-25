using FluentValidation;
using Kromplon.Commons.Abstractions.Result;
using Kromplon.Modules.Users.Core.Models;
using Kromplon.Modules.Users.Core.Services;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace Kromplon.Modules.Users.Core.Commands.SingIn;

public class SignInCommandHandler(
    IValidator<SignInCommand> validator,
    UserManager<User> userManager,
    ITokenService tokenService)
    : ICommandHandler<SignInCommand, Result<TokensDto>>
{
    public async ValueTask<Result<TokensDto>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<TokensDto>.Failure(ErrorType.ValidationError, string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage)));
        }
        var user = await userManager.FindByEmailAsync(command.SignInDto.Email);
        if (user == null)
        {
            return Result<TokensDto>.Failure(ErrorType.NotFound, "Invalid password");
        }
        
        var isPasswordValid = await userManager.CheckPasswordAsync(user, command.SignInDto.Password);
        if (!isPasswordValid)
        {
            return Result<TokensDto>.Failure(ErrorType.NotFound, "Invalid password");
        }
        
        var token = await tokenService.GenerateJwtToken(user);

        return Result<TokensDto>.Success(new TokensDto(token));
    }
}