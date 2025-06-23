using Kromplon.Commons.Abstractions.Result;
using Mediator;

namespace Kromplon.Modules.Users.Core.Commands.AddUser;

public sealed record AddUserCommand(RegisterUserDto RegisterUserDto) : ICommand<Result>;

public sealed record RegisterUserDto(string Email, string Password, string ConfirmPassword);