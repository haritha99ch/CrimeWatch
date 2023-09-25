namespace CrimeWatch.Application.Primitives;
public abstract class RequestPermissions
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly UserClaims UserClaims;
    private HttpContext HttpContext => _httpContextAccessor.HttpContext ?? throw new Exception("Not an API");

    protected RequestPermissions(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        UserClaims = HttpContext.GetUserClaims();
    }
}
