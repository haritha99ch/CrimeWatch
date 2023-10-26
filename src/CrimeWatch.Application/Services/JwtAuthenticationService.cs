using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Application.Shared;

namespace CrimeWatch.Application.Services;
internal class JwtAuthenticationService : IAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly JwtOptions _jwtOptions;
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public JwtAuthenticationService(
        IHttpContextAccessor httpContextAccessor,
        IOptions<JwtOptions> jwtOptions,
        IRepository<Moderator, ModeratorId> moderatorRepository,
        IRepository<Witness, WitnessId> witnessRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _moderatorRepository = moderatorRepository;
        _witnessRepository = witnessRepository;
        _jwtOptions = jwtOptions.Value;
    }

    private HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public AuthenticationResult Authenticate() => GetAuthenticationResult();

    public async Task<string> AuthenticateAndGetToken(Account account, CancellationToken cancellationToken)
    {
        if (account.IsModerator)
        {
            var moderator =
                await _moderatorRepository.GetModeratorWithAllByAccountIdAsync(account.Id, cancellationToken);
            return GenerateToken(account.IsModerator, moderator!.Id.Value, account.Email);
        }
        var witness = await _witnessRepository.GetWitnessWithAllByAccountIdAsync(account.Id, cancellationToken);
        return GenerateToken(account.IsModerator, witness!.Id.Value, account.Email);
    }

    private string GenerateToken(bool isModerator, Guid id, string email)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, isModerator ? nameof(Moderator) : nameof(Witness)),
            new(JwtRegisteredClaimNames.Sub, id.ToString()),
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

    private AuthenticationResult GetAuthenticationResult()
    {
        ArgumentNullException.ThrowIfNull(HttpContext, paramName: "Not an API");
        var claims = HttpContext.User.Claims.ToList();

        var userId = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.NameIdentifier));
        var userRole = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Role));

        UserClaims userClaims = new(userId?.Value, userRole?.Value ?? "");
        return userClaims.UserType switch
        {
            UserType.Moderator => userClaims.ModeratorId!,
            UserType.Witness => userClaims.WitnessId!,
            _ => true
        };
    }
}
