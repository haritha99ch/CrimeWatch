﻿namespace Application.Selectors.Accounts;
public sealed record AccountAuthenticationInfo(AccountId AccountId, bool IsModerator)
    : Selector<Account, AccountAuthenticationInfo>
{
    protected override Expression<Func<Account, AccountAuthenticationInfo>> MapQueryableSelector() =>
        e => new(e.Id, e.AccountType.Equals(AccountType.Moderator));
}