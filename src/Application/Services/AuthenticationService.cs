using Application.Helpers.Repositories;
using Application.Specifications.Accounts;
using ApplicationSettings.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services;
internal class AuthenticationService : IAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly JwtOptions _jwtOptions;
    private HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public AuthenticationService(
            IHttpContextAccessor httpContextAccessor,
            IRepository<Account, AccountId> accountRepository,
            IOptions<JwtOptions> jwtOptions
        )
    {
        _httpContextAccessor = httpContextAccessor;
        _accountRepository = accountRepository;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<Result<string>> AuthenticateAndGetTokenAsync(
            string email,
            string password,
            CancellationToken cancellationToken
        )
    {
        var account = await _accountRepository.GetAccountByEmail(email, cancellationToken);
        if (account is null || !account.VerifyPassword(password)) return AccountNotFoundError.Create();

        return GenerateToken(account.AccountType.Equals(AccountType.Moderator), account.Id, email);
    }

    public Task<string> RefreshToken(string token, string refreshToken) => throw new NotImplementedException();

    private string GenerateToken(bool isModerator, AccountId id, string email)
    {
        var claims = new List<Claim>
        {
            new(
                ClaimTypes.Role,
                isModerator ? AccountType.Moderator.ToString() : AccountType.Witness.ToString()),
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
            signingCredentials: signInCredentials);
        return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    }

    public async Task<Result<AccountAuthenticationInfo>> GetAuthenticationResultAsync(
            CancellationToken cancellationToken
        )
    {
        ArgumentNullException.ThrowIfNull(HttpContext, paramName: "Not an API");
        var claims = HttpContext.User.Claims.ToList();
        var accountIdValue = claims
            .Where(
                e =>
                    e.Type.Equals(ClaimTypes.NameIdentifier)
                    || e.Type.Equals(JwtRegisteredClaimNames.Sub))
            .Select(e => e.Value)
            .FirstOrDefault();

        if (accountIdValue is null) return UnableToAuthenticateError.Create(message: "User has not signIn.");

        var accountId = new AccountId(new(accountIdValue));

        var account = await _accountRepository
            .GetOneAsync<AccountAuthenticationInfoById, AccountAuthenticationInfo>(
                new(accountId),
                cancellationToken);

        return account is null
            ? UnableToAuthenticateError.Create(message: "Authenticated user is not found.")
            : account;
    }
}
