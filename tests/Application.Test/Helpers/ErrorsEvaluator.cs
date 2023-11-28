using Application.Common.Errors;

namespace Application.Test.Helpers;

internal static class ErrorsEvaluator
{
    internal static bool HasError<TError>(this Error error) => error is TError;
}
