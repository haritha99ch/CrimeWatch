namespace Shared.Models.Accounts;
public class AccountAuthenticationInfo : ISelector
{
    public required AccountId AccountId { get; init; }
    public bool IsModerator { get; init; }
}
