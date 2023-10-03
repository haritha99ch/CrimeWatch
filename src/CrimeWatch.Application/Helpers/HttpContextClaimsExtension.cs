using CrimeWatch.Application.Primitives;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CrimeWatch.Application.Helpers;
internal static class HttpContextClaimsExtension
{
    public static UserClaims GetUserClaims(this HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context, paramName: "Not an API");
        var claims = context.User.Claims.ToList();

        var userId = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.NameIdentifier));
        var userRole = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Role));

        return new(userId?.Value, userRole?.Value ?? "");
    }
}
