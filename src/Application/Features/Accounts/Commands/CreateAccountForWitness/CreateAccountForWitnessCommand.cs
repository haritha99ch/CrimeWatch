namespace Application;

public sealed record CreateAccountForWitnessCommand(
    string Nic,
    string FirstName,
    string LastName,
    Gender Gender,
    DateOnly BirthDay,
    string Email,
    string Password,
    string PhoneNumber
) : IRequest<Result<Account>>;
