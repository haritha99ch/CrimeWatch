using Persistence.Common.Specifications;

namespace Application.Specifications.Accounts;
internal sealed class AccountAuthenticationInfoById : Specification<Account, AccountAuthenticationInfo>
{
    public AccountAuthenticationInfoById(AccountId accountId) : base(e => e.Id.Equals(accountId))
    {
        ProjectTo(e => new()
        {
            AccountId = e.Id,
            IsModerator = e.AccountType.Equals(AccountType.Moderator)
        });
    }
}
