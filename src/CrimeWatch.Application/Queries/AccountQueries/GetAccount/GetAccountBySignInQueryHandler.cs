using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Application.Queries.AccountQueries.GetAccount;
internal class GetAccountBySignInQueryHandler : IRequestHandler<GetAccountBySignInQuery, string>
{
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IAuthenticationService _authenticationService;
    public GetAccountBySignInQueryHandler(
        IRepository<Account, AccountId> accountRepository,
        IAuthenticationService authenticationService)
    {
        _accountRepository = accountRepository;
        _authenticationService = authenticationService;
    }

    public async Task<string> Handle(GetAccountBySignInQuery request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountBySignInAsync(
            request.Email,
            request.Password,
            cancellationToken);

        return await _authenticationService.AuthenticateAndGetToken(
            account ?? throw new("Account not found"),
            cancellationToken);
    }
}
