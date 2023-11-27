namespace Shared.Requests.Accounts;

public sealed record CreateAccountForModeratorRequest(
    string Nic,
    string FirstName,
    string LastName,
    DateTime BirthDay,
    Gender Gender,
    string Email,
    string Password,
    string PhoneNumber,
    string PoliceId,
    string City,
    string Province
);
