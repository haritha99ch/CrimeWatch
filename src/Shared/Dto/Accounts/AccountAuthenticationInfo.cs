namespace Shared.Dto.Accounts;
public class AccountAuthenticationInfo
{
    public required AccountId AccountId { get; init; }
    public bool IsModerator { get; init; }
}
