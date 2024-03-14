using Persistence.Common.Specifications;

namespace Application.Specifications.Accounts;
internal record AccountAuthenticationInfoById : Specification<Account, AccountAuthenticationInfo>
{
    public AccountAuthenticationInfoById(AccountId accountId) : base(e => e.Id.Equals(accountId))
    {
        ProjectTo(GetProjection<Account, AccountAuthenticationInfo>());
    }
}
