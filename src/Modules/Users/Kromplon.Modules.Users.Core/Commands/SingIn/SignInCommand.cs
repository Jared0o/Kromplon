using Kromplon.Commons.Abstractions.Result;
using Mediator;

namespace Kromplon.Modules.Users.Core.Commands.SingIn;

public sealed record SignInCommand(SignInDto SignInDto) : ICommand<Result<TokensDto>>;

public sealed record SignInDto(string Email, string Password);

public sealed record TokensDto(string AccessToken);