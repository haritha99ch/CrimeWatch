namespace Application.Features.Accounts.Commands.DeleteAccount;
sealed internal class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand, bool>
{
    private readonly IRepository<Account, AccountId> _accountRepository;

    public DeleteAccountCommandHandler(IRepository<Account, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Result<bool>> Handle(
            DeleteAccountCommand request,
            CancellationToken cancellationToken
        )
    {
        var accountExists = await _accountRepository.ExistByIdAsync(
            request.AccountId,
            cancellationToken);
        if (!accountExists) return AccountNotFoundError.Create();

        var deleted = await _accountRepository.DeleteByIdAsync(
            request.AccountId,
            cancellationToken);
        if (!deleted) return AccountCouldNotBeDeletedError.Create();
        return true;
    }
}
