namespace Application.Selectors.Accounts;
public record AccountInfo(string FullName, string Email, string PhoneNumber, bool IsModerator)
    : Selector<Account, AccountInfo>
{
    protected override Expression<Func<Account, AccountInfo>> MapQueryableSelector() =>
        e =>
            new(
                $"{e.Person!.FirstName} {e.Person.LastName}",
                e.Email,
                e.PhoneNumber,
                e.AccountType.Equals(AccountType.Moderator));
}
