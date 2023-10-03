namespace CrimeWatch.Application.Primitives;
public abstract class RequestPermissions
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly UserClaims UserClaims;

    protected RequestPermissions(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        UserClaims = HttpContext.GetUserClaims();
    }

    private HttpContext HttpContext => _httpContextAccessor.HttpContext ?? throw new("Not an API");
}
