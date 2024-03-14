using System.Linq.Expressions;

namespace Application.Selectors.Accounts;
public record AccountInfo(string FullName, string Email, string PhoneNumber, bool IsModerator)
    : ISelector<Account, AccountInfo>
{
    public Expression<Func<Account, AccountInfo>> SetProjection()
        => e =>
            new(
                $"{e.Person!.FirstName} {e.Person.LastName}",
                e.Email,
                e.PhoneNumber,
                e.AccountType.Equals(AccountType.Moderator));
}
