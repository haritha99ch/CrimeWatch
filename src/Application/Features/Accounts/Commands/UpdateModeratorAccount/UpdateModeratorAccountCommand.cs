namespace Application.Features.Accounts.Commands.UpdateModeratorAccount;

public sealed record UpdateModeratorAccountCommand(
    AccountId AccountId,
    string Nic,
    string FirstName,
    string LastName,
    Gender Gender,
    DateOnly BirthDay,
    string PoliceId,
    string City,
    string Province
) : ICommand<Account>;
