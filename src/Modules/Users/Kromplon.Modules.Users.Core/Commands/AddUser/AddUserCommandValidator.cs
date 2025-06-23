using FluentValidation;

namespace Kromplon.Modules.Users.Core.Commands.AddUser;

public class AddUserCommandValidator: AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.RegisterUserDto.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.RegisterUserDto.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        RuleFor(x => x.RegisterUserDto.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Confirm Password is required")
            .Equal(x => x.RegisterUserDto.Password)
            .WithMessage("Passwords do not match");
    }
}