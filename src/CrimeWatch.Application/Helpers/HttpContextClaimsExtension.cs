using CrimeWatch.Application.Primitives;

namespace CrimeWatch.Application.Helpers;
internal static class HttpContextClaimsExtension
{
    public static UserClaims GetUserClaims(this HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context, "Not an API");
        var claims = context.User.Claims;

        var UserId = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.NameIdentifier));
        var UserRole = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Role));

        return new(UserId?.Value, UserRole?.Value ?? "");
    }
}
