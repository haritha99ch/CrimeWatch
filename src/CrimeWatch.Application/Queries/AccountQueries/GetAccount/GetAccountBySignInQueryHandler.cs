using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Application.Queries.AccountQueries.GetAccount;
internal class GetAccountBySignInQueryHandler : IRequestHandler<GetAccountBySignInQuery, Account>
{
    private readonly IRepository<Account, AccountId> _accountRepository;

    public GetAccountBySignInQueryHandler(IRepository<Account, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> Handle(GetAccountBySignInQuery request, CancellationToken cancellationToken)
        => await _accountRepository.GetAccountBySignInAsync(request.Email, request.Password, cancellationToken)
        ?? throw new Exception("Account not found");
}
