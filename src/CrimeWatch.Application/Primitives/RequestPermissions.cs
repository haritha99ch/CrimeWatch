namespace CrimeWatch.Application.Primitives;
internal abstract class RequestPermissions
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    protected UserClaims _userClaims;
    private HttpContext _httpContext => _httpContextAccessor.HttpContext ?? throw new Exception("Not an API");

    public RequestPermissions(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _userClaims = _httpContext.GetUserClaims();
    }
}
