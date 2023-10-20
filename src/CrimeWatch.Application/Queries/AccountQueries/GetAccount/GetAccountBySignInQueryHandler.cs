using CrimeWatch.Domain.AggregateModels.AccountAggregate;
using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;
using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Queries.AccountQueries.GetAccount;
internal class GetAccountBySignInQueryHandler : IRequestHandler<GetAccountBySignInQuery, string>
{
    private readonly IRepository<Account, AccountId> _accountRepository;

    private readonly JwtOptions _jwtOptions;
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public GetAccountBySignInQueryHandler(
            IRepository<Account, AccountId> accountRepository,
            IRepository<Witness, WitnessId> witnessRepository,
            IRepository<Moderator, ModeratorId> moderatorRepository,
            IOptions<JwtOptions> jwtOptions
        )
    {
        _accountRepository = accountRepository;
        _witnessRepository = witnessRepository;
        _moderatorRepository = moderatorRepository;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<string> Handle(GetAccountBySignInQuery request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountBySignInAsync(
            request.Email,
            request.Password,
            cancellationToken);
        return await Authenticate(account ?? throw new("Account not found"), cancellationToken);
    }

    private async Task<string> Authenticate(Account account, CancellationToken cancellationToken)
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
}
