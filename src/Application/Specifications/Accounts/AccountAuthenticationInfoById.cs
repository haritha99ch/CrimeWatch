using Persistence.Common.Specifications;

namespace Application.Specifications.Accounts;
internal record AccountAuthenticationInfoById : Specification<Account, AccountAuthenticationInfo>
{
    public AccountAuthenticationInfoById(AccountId accountId) : base(e => e.Id.Equals(accountId))
    {
        Select = e => new AccountAuthenticationInfo(e.Id, e.AccountType.Equals(AccountType.Moderator));
    }
}
