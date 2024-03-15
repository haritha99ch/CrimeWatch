namespace Shared.Dto.Accounts;
public class AccountInfo
{
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public bool IsModerator { get; init; }
}
