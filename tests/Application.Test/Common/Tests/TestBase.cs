using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Test.Common.Host;
using ApplicationSettings.Options;
using Domain.AggregateModels.AccountAggregate.Enums;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Test.Common.Tests;

/// <summary>
///     To Authorize, call <see cref="GenerateTokenAndInvoke(bool, AccountId, string)."/>
/// </summary>
public abstract class TestBase
{
    private readonly App _app = App.Create();
    protected ApplicationDbContext DbContext => _app.DbContext;

    protected ISender Mediator => _app.GetRequiredService<ISender>();
    private IOptions<JwtOptions> _jwtOptionsInstance =>
        _app.GetRequiredService<IOptions<JwtOptions>>();
    private JwtOptions _jwtOptions => _jwtOptionsInstance.Value;
    private HttpContext _httpContext =>
        _app.GetRequiredService<IHttpContextAccessor>().HttpContext!;

    protected virtual async Task InitializeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
    }

    protected virtual async Task CleanupAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
    }

    protected async Task SaveAndClearChangeTrackerAsync()
    {
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();
    }

    protected string GenerateTokenAndInvoke(bool isModerator, AccountId id, string email)
    {
        var claims = new List<Claim>
        {
            new(
                ClaimTypes.Role,
                isModerator ? AccountType.Moderator.ToString() : AccountType.Moderator.ToString()
            ),
            new(JwtRegisteredClaimNames.Sub, id.Value.ToString()),
            new(JwtRegisteredClaimNames.Email, email)
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokeOptions = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: signInCredentials
        );

        var identity = new ClaimsIdentity(claims, "AuthenticationTypes.Federation");
        _httpContext.User.AddIdentity(identity);

        return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    }
}
