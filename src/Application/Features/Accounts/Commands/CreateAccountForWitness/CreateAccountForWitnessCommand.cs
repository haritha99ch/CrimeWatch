namespace Application.Features.Accounts.Commands.CreateAccountForWitness;
public sealed record CreateAccountForWitnessCommand(
        string Nic,
        string FirstName,
        string LastName,
        Gender Gender,
        DateTime BirthDay,
        string Email,
        string Password,
        string PhoneNumber
    ) : ICommand<AccountInfo>;
