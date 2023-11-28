using System.Linq.Expressions;
using Persistence.Common.Selectors;

namespace Application.Selectors;

public sealed record AccountAuthenticationInfo(AccountId AccountId, bool IsModerator)
    : Selector<Account, AccountAuthenticationInfo>
{
    protected override Expression<Func<Account, AccountAuthenticationInfo>> Select() =>
        e => new(e.Id, e.AccountType.Equals(AccountType.Moderator));
}
