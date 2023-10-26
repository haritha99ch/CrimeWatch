using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Queries.AccountQueries.GetAccount;
internal class GetAccountBySignInQueryHandler(
        IRepository<Account, AccountId> accountRepository,
        IAuthenticationService authenticationService)
    : IRequestHandler<GetAccountBySignInQuery, string>
{

    public async Task<string> Handle(GetAccountBySignInQuery request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetAccountBySignInAsync(
            request.Email,
            request.Password,
            cancellationToken);

        return await authenticationService.AuthenticateAndGetToken(
            account ?? throw new("Account not found"),
            cancellationToken);
    }
}
