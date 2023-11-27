using Application.Common.Errors;

namespace Application.Common.Results;

public readonly struct Result<T>
{
    private bool IsSuccess => _error is null && _value is not null;
    private readonly Error? _error;
    private readonly T? _value;

    private Result(T value)
    {
        _value = value;
    }

    private Result(Error error)
    {
        _error = error;
    }

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(Error error) => new(error);

    public TResult? GetResult<TResult>(
        Func<T, TResult> onSuccess,
        Func<Error, TResult>? onError = null
    )
        where TResult : class
    {
        if (IsSuccess)
        {
            return onSuccess(_value ?? throw new("Value is null on success"));
        }
        else if (onError is not null)
        {
            return onError(_error ?? throw new("Error is null on fail"));
        }
        return null;
    }
}
