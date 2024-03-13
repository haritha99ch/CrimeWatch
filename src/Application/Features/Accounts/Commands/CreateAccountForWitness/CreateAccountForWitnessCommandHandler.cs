namespace Application.Features.Accounts.Commands.CreateAccountForWitness;
internal sealed class CreateAccountForWitnessCommandHandler
    : ICommandHandler<CreateAccountForWitnessCommand, Account>
{
    private readonly IRepository<Account, AccountId> _accountRepository;

    public CreateAccountForWitnessCommandHandler(IRepository<Account, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Result<Account>> Handle(
            CreateAccountForWitnessCommand request,
            CancellationToken cancellationToken
        )
    {
        var account = Account.CreateAccountForWitness(
            request.Nic,
            request.FirstName,
            request.LastName,
            request.Gender,
            new(request.BirthDay.Year, request.BirthDay.Month, request.BirthDay.Day),
            request.Email,
            request.Password,
            request.PhoneNumber);
        return await _accountRepository.AddAsync(account, cancellationToken);
    }
}
