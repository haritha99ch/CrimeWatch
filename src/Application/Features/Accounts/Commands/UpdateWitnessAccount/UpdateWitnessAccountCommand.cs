namespace Application.Features.Accounts.Commands.UpdateWitnessAccount;

public sealed record UpdateWitnessAccountCommand(
    AccountId AccountId,
    string Nic,
    string FirstName,
    string LastName,
    Gender Gender,
    DateOnly BirthDay
) : ICommand<Account>;
