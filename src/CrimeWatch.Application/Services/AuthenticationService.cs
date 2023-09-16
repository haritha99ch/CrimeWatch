using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CrimeWatch.Application.Services;
internal class AuthenticationService : IAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private HttpContext _httpClient => _httpContextAccessor.HttpContext!;

    public AuthenticationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public (bool, string?) Authenticate()
    {
        var claims = _httpClient.User.Claims;
        var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        var idClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        bool isModerator = roleClaim?.Value == nameof(Moderator);

        return (isModerator, idClaim?.Value);
    }
}
