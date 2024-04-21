namespace Shared.Models.Accounts;
public class AccountInfo : ISelector
{
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public bool IsModerator { get; init; }
}
