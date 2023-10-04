using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Services;
internal class AuthenticationService : IAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext _httpClient => _httpContextAccessor.HttpContext!;

    public (bool, string?) Authenticate()
    {
        var userClaims = _httpClient.GetUserClaims();
        return (userClaims.UserType.Equals(UserType.Moderator), userClaims.UserId);
    }
}
