using Persistence.Common.Specifications;

namespace Application.Specifications.Accounts;
internal sealed class AccountInfoById : Specification<Account, AccountInfo>
{
    public AccountInfoById(AccountId accountId) : base(e => e.Id.Equals(accountId))
    {
        ProjectTo(e => new()
        {
            FullName = $"{e.Person!.FirstName} {e.Person.LastName}",
            Email = e.Email,
            PhoneNumber = e.PhoneNumber,
            IsModerator = e.AccountType.Equals(AccountType.Moderator)
        });
    }

}
