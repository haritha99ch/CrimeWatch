using Application.Helpers.Errors;
using FluentValidation;

namespace Application.Behaviors;
internal sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
    {
        if (!_validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(async e => await e.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(e => e.Errors).Where(e => e != null).ToList();

        if (failures.Count == 0) return await next();

        var error = failures.FirstOrDefault();
        var userDefinedErrorObject = error?.CustomState;
        if (userDefinedErrorObject is Error userDefinedError)
        {
            return userDefinedError.ToTypedValidationError<TResponse>();
        }
        var validationError = ValidationError.Create(message: error?.ErrorMessage);
        return validationError.ToTypedValidationError<TResponse>();
    }
}
