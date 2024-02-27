using Persistence.Common.Specifications;

namespace Application.Specifications.Accounts;
internal record AccountInfoById : Specification<Account, AccountInfo>
{
    public AccountInfoById(AccountId accountId) : base(e => e.Id.Equals(accountId))
    {
        Select = AccountInfo.GetProjection;
    }

}
