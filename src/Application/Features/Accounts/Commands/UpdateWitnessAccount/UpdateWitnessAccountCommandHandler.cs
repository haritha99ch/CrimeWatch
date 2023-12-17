using Application.Helpers.Repositories;

namespace Application.Features.Accounts.Commands.UpdateWitnessAccount;
sealed internal class UpdateWitnessAccountCommandHandler
    : ICommandHandler<UpdateWitnessAccountCommand, Account>
{
    private readonly IRepository<Account, AccountId> _accountRepository;

    public UpdateWitnessAccountCommandHandler(IRepository<Account, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Result<Account>> Handle(
            UpdateWitnessAccountCommand request,
            CancellationToken cancellationToken
        )
    {
        var account = await _accountRepository.GetWitnessAccountIncludingOwnedById(
            request.AccountId,
            cancellationToken);
        if (account is null) return AccountNotFoundError.Create(message: "No account found to update.");

        account.UpdateWitness(
            request.Nic,
            request.FirstName,
            request.LastName,
            request.Gender,
            request.BirthDay);
        return await _accountRepository.UpdateAsync(account, cancellationToken);
    }
}
