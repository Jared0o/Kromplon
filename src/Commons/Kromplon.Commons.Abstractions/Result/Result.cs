namespace Kromplon.Commons.Abstractions.Result;

#pragma warning disable CA1716
public class Error
#pragma warning restore CA1716
    (ErrorType type, string message)
{
    public ErrorType Type { get; } = type;
    public string Message { get; } = message;
}

public class Result<T>
{
    public bool IsSuccess { get; }
    public Error? Error { get; }
    public T? Value { get; }

    protected Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(ErrorType errorType, string errorMessage) => new(new Error(errorType, errorMessage));
}

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    protected Result(bool isSuccess, Error? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true);
    public static Result Failure(ErrorType errorType, string errorMessage) => new(false, new Error(errorType, errorMessage));
}