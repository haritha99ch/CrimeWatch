namespace CrimeWatch.Application.Common;
public abstract class HttpContextValidator<TRequest> : AbstractValidator<TRequest> where TRequest : IBaseRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly UserClaims UserClaims;

    protected HttpContextValidator(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        UserClaims = HttpContext.GetUserClaims();
    }

    private HttpContext HttpContext => _httpContextAccessor.HttpContext ?? throw new("Not an API");
}
