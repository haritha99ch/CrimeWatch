using System.Linq.Expressions;

namespace Application.Selectors.Accounts;
public sealed record AccountAuthenticationInfo(AccountId AccountId, bool IsModerator)
    : ISelector<Account, AccountAuthenticationInfo>
{
    public Expression<Func<Account, AccountAuthenticationInfo>> SetProjection() =>
        e => new(e.Id, e.AccountType.Equals(AccountType.Moderator));
}
