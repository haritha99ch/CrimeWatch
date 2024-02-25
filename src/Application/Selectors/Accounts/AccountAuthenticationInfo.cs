namespace Application.Selectors.Accounts;
public sealed record AccountAuthenticationInfo(AccountId AccountId, bool IsModerator) : ISelector;
