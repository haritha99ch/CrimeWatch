using Application.Common.Errors;

namespace Application.Common.Results;

public readonly struct Result<T>
{
    private bool IsSuccess => _error is null && _result is not null;
    private readonly Error? _error;
    private readonly T? _result;

    private Result(T result)
    {
        _result = result;
    }

    private Result(Error error)
    {
        _error = error;
    }

    public static implicit operator Result<T>(T result) => new(result);

    public static implicit operator Result<T>(Error error) => new(error);

    public TResult? GetValue<TResult>(
        Func<T, TResult> onSuccess,
        Func<Error, TResult>? onError = null
    )
        where TResult : class
    {
        if (!IsSuccess)
        {
            if (onError is null)
                throw new ArgumentNullException(
                    nameof(onError),
                    $"The operation has failed with an error '{_error?.GetType()}', but no error handler has been provided."
                );
            return onError(
                _error
                    ?? throw new InvalidOperationException(
                        "The operation has failed, but no error information is available."
                    )
            );
        }

        return onSuccess(
            _result
                ?? throw new InvalidOperationException(
                    "The operation was successful, but no result is available."
                )
        );
    }

    public T GetValue()
    {
        if (!IsSuccess)
            throw new InvalidOperationException(
                $"The operation has failed with an error '{_error?.GetType()}'.\nUse GetValue<TResult> method to handle error."
            );
        if (_result is null)
            throw new ArgumentNullException(
                nameof(_result),
                "The operation was successful, but no result is available."
            );
        return _result;
    }

    public Error GetError()
    {
        if (IsSuccess)
            throw new InvalidOperationException(
                $"The operation was successful. No error information is available."
            );
        if (_error is null)
            throw new ArgumentNullException(nameof(_error), "No error information is available.");
        return _error;
    }
}
