namespace Application.Features.Accounts.Commands.CreateAccountForModerator;

public sealed record CreateAccountForModeratorCommand(
    string Nic,
    string FirstName,
    string LastName,
    Gender Gender,
    DateTime BirthDay,
    string PoliceId,
    string City,
    string Province,
    string Email,
    string Password,
    string PhoneNumber
) : ICommand<Account>;
