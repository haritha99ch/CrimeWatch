using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Common;
public abstract class HttpContextValidator<TRequest> : AbstractValidator<TRequest> where TRequest : IBaseRequest
{
    protected readonly IAuthenticationService _authenticationService;

    protected HttpContextValidator(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
}
