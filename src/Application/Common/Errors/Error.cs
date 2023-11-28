namespace Application.Common.Errors;

public abstract record Error<TError> : Error
    where TError : Error<TError>, new()
{
    internal static TError Create(string? title = null, string? message = null, int? code = null) =>
        new()
        {
            Title = title ?? default!,
            Message = message ?? default!,
            Code = code ?? default!
        };
}

public abstract record Error
{
    public abstract string Title { get; init; }
    public abstract string Message { get; init; }
    public abstract int Code { get; init; }
}
