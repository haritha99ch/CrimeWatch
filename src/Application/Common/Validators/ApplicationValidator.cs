using FluentValidation;

namespace Application.Common.Validators;
/// <summary>
///     Call WithState( e=> validationError) to return error from <see cref="Result{T}." />
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <summary>
/// </summary>
/// <typeparam name="TRequest"></typeparam>
public abstract class ApplicationValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : IBaseRequest
{
    protected Error? validationError { get; set; } = default;
}
