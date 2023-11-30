using Application.Common.Errors;

namespace Application.Common.Results;

public class Result<T> : Result
{
    private bool IsSuccess => _error is null && _result is not null;
    private readonly T? _result;

    private Result(T result)
    {
        _result = result;
    }

    private Result(Error error)
        : base(error) { }

    public static implicit operator Result<T>(T result) => new(result);

    public static implicit operator Result<T>(Error error) => new(error);

    public static Result<T> Failure(Error error)
    {
        return new Result<T>(error);
    }

    public TResult? Handle<TResult>(
        Func<T, TResult> onSuccess,
        Func<Error, TResult>? onError = null
    )
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

public class Result
{
    protected readonly Error? _error;

    protected Result(Error? error = default)
    {
        _error = error;
    }
}
