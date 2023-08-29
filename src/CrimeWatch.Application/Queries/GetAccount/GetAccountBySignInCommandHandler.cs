using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Application.Queries.GetAccount;
internal class GetAccountBySignInCommandHandler : IRequestHandler<GetAccountBySignInCommand, Account>
{
    private readonly IRepository<Account, AccountId> _accountRepository;

    public GetAccountBySignInCommandHandler(IRepository<Account, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> Handle(GetAccountBySignInCommand request, CancellationToken cancellationToken)
        => await _accountRepository.GetByAsync<AccountBySignIn>(new(request.Email, request.Password), cancellationToken)
        ?? throw new Exception("Account not found");
}
