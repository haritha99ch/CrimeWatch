using Persistence.Common.Specifications;

namespace Application.Specifications.AccountSpecifications;

internal sealed record AccountByEmail : Specification<Account>
{
    public AccountByEmail(string email)
        : base(e => e.Email.Equals(email)) { }
}
