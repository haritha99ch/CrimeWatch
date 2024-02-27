using Persistence.Common.Selectors;
using System.Linq.Expressions;

namespace Application.Selectors.Accounts;
public sealed record AccountAuthenticationInfo(AccountId AccountId, bool IsModerator)
    : Selector<Account, AccountAuthenticationInfo>, ISelector
{
    protected override Expression<Func<Account, AccountAuthenticationInfo>> SetProjection() =>
        e => new(e.Id, e.AccountType.Equals(AccountType.Moderator));
}
