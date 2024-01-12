namespace Application.Helpers.Errors;
public static class ErrorExtension
{
    internal static TResult ToTypedValidationError<TResult>(this Error error)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationError.Create(error.Title, error.Message, error.Code) as TResult)!;
        }
        var validationResults = (TResult)
            typeof(Result<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(Result<object>.Failure))!
                .Invoke(null, [error])!;

        return validationResults;
    }
}
