namespace Application.Features.Accounts.Commands.CreateAccountForModerator;
public sealed class CreateAccountForModeratorCommandHandler
    : ICommandHandler<CreateAccountForModeratorCommand, Account>
{
    private readonly IRepository<Account, AccountId> _accountRepository;

    public CreateAccountForModeratorCommandHandler(
            IRepository<Account, AccountId> accountRepository
        )
    {
        _accountRepository = accountRepository;
    }

    public async Task<Result<Account>> Handle(
            CreateAccountForModeratorCommand request,
            CancellationToken cancellationToken
        )
    {
        var account = Account.CreateAccountForModerator(
            request.Nic,
            request.FirstName,
            request.LastName,
            request.Gender,
            new(request.BirthDay.Year, request.BirthDay.Month, request.BirthDay.Day),
            request.PoliceId,
            request.City,
            request.Province,
            request.Email,
            request.Password,
            request.PhoneNumber);

        return await _accountRepository.AddAsync(account, cancellationToken);
    }
}
