namespace Application.Common.Errors;

/// <summary>
/// Implements factory method <see cref="Error{TError}.Create"/> for errors to overwrite error properties.
/// </summary>
/// <returns></returns>
public abstract record Error<TError> : Error
    where TError : Error<TError>, new()
{
    internal static TError Create(
        string? title = null,
        string? message = null,
        HttpStatusCode? code = null
    )
    {
        var error = new TError();
        return new TError() with
        {
            Title = title ?? error.Title,
            Message = message ?? error.Message,
            Code = code ?? error.Code
        };
    }
}

public abstract record Error
{
    public abstract string Title { get; init; }
    public abstract string Message { get; init; }
    public abstract HttpStatusCode Code { get; init; }
}
