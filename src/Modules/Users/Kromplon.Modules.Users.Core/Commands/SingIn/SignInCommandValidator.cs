using FluentValidation;

namespace Kromplon.Modules.Users.Core.Commands.SingIn;

public sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.SignInDto.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.SignInDto.Password).NotEmpty().WithMessage("Password is required");
    }
}