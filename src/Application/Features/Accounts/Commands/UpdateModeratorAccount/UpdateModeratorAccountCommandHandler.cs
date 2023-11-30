using Application.Helpers.Repositories;

namespace Application.Features.Accounts.Commands.UpdateModeratorAccount;

internal sealed class UpdateModeratorAccountCommandHandler
    : ICommandHandler<UpdateModeratorAccountCommand, Account>
{
    private readonly IRepository<Account, AccountId> _accountRepository;

    public UpdateModeratorAccountCommandHandler(IRepository<Account, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Result<Account>> Handle(
        UpdateModeratorAccountCommand request,
        CancellationToken cancellationToken
    )
    {
        var account = await _accountRepository.GetModeratorAccountIncludingOwnedById(
            request.AccountId,
            cancellationToken
        );
        if (account is null)
            return AccountNotFoundError.Create(message: "No account found to update.");

        account.UpdateModerator(
            request.Nic,
            request.FirstName,
            request.LastName,
            request.Gender,
            request.BirthDay,
            request.PoliceId,
            request.City,
            request.Province
        );
        return await _accountRepository.UpdateAsync(account, cancellationToken);
    }
}
