using Kromplon.Commons.Abstractions.Result;
using Microsoft.AspNetCore.Http;

namespace Kromplon.Commons.Infrastructure.Api;

public static class ApiResult
{
    public static IResult HandleError(Error? error)
    {
        return error?.Type switch
        {
            ErrorType.NotFound => Results.NotFound(new { Error = error.Type.ToString(), Details = error }),
            ErrorType.ValidationError => Results.BadRequest(new { Error = error.Type.ToString(), Details = error }),
            _ => Results.BadRequest(new { Error = error?.Type.ToString() ?? "Unknown", Details = error })
        };
    }
}