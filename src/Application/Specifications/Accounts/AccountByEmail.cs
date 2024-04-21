using Persistence.Common.Specifications;

namespace Application.Specifications.Accounts;
internal sealed class AccountByEmail : Specification<Account>
{
    public AccountByEmail(string email)
        : base(e => e.Email.Equals(email)) { }
}
