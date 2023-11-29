using Application.Features.Accounts.Queries.SignInToAccount.Errors;

namespace Application.Features.Accounts.Queries.GetAccountById;

internal sealed class GetAccountByIdQueryHandler
    : IQueryHandler<GetAccountByIdQuery, Account>
{
    private readonly IRepository<Account, AccountId> _accountRepository;

    public GetAccountByIdQueryHandler(IRepository<Account, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Result<Account>> Handle(
        GetAccountByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var account = await _accountRepository.GetByIdAsync(request.AccountId, cancellationToken);
        return account is not null
            ? account
            : AccountNotFoundError.Create(
                message: $"Account is not found with Id:{request.AccountId.Value}"
            );
    }
}
