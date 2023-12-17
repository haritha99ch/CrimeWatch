using Application.Common.Errors;

namespace Application.Test.Helpers;
internal static class ErrorsEvaluator
{
    internal static bool Is<TError>(this Error error) => error is TError;
}
