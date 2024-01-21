namespace Application.Common.Results;
public sealed class Result<T> : Result
{
    private bool IsSuccess => _error is null && _value is not null;
    private readonly T? _value;

    private Result(T value)
    {
        _value = value;
    }

    private Result(Error error) : base(error) { }

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);
    public static Result<T> Failure(Error error) => new(error);

    public TResult Handle<TResult>(
            Func<T, TResult> onSuccess,
            Func<Error, TResult>? onError = null
        )
    {
        if (IsSuccess)
            return onSuccess(
                _value
                ?? throw new InvalidOperationException("The operation was successful, but no result is available."));
        if (onError is null)
            throw new ArgumentNullException(
                nameof(onError),
                $"The operation has failed with an error '{_error?.GetType()}', but no error handler has been provided.");
        return onError(
            _error
            ?? throw new InvalidOperationException(
                "The operation has failed, but no error information is available."));
    }

    public void Handle(Action<T> onSuccess, Action<Error>? onError = null)
    {
        if (IsSuccess)
            onSuccess(
                _value
                ?? throw new InvalidOperationException("The operation was successful, but no result is available."));
        onError?.Invoke(_error
            ?? throw new InvalidOperationException(
                "The operation has failed, but no error information is available."));
    }

    public T GetValue()
    {
        if (!IsSuccess)
            throw new InvalidOperationException(
                $"The operation has failed with an error '{_error?.GetType()}'.\nUse GetValue<TResult> method to handle error.");
        if (_value is null)
            throw new ArgumentNullException(
                nameof(_value),
                message: "The operation was successful, but no result is available.");
        return _value;
    }

    public Error GetError()
    {
        if (IsSuccess)
            throw new InvalidOperationException("The operation was successful. No error information is available.");
        if (_error is null)
            throw new ArgumentNullException(nameof(_error), message: "No error information is available.");
        return _error;
    }
}

public abstract class Result(Error? error = default)
{
    protected readonly Error? _error = error;
}
