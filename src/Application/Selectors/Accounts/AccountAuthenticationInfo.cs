using System.Linq.Expressions;

namespace Application.Selectors.Accounts;
public sealed class AccountAuthenticationInfo
    : AccountDto.AccountAuthenticationInfo, ISelector<Account, AccountAuthenticationInfo>
{
    public Expression<Func<Account, AccountAuthenticationInfo>> SetProjection()
        => e => new()
        {
            AccountId = e.Id,
            IsModerator = e.AccountType.Equals(AccountType.Moderator)
        };
}
