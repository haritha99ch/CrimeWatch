using System.Linq.Expressions;

namespace Application.Selectors.Accounts;
public sealed class AccountInfo
    : AccountDto.AccountInfo, ISelector<Account, AccountInfo>
{
    public Expression<Func<Account, AccountInfo>> SetProjection()
        => e => new()
        {
            FullName = $"{e.Person!.FirstName} {e.Person.LastName}",
            Email = e.Email,
            PhoneNumber = e.PhoneNumber,
            IsModerator = e.AccountType.Equals(AccountType.Moderator)
        };
}
