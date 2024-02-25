using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.Accounts;
internal sealed record WitnessAccountToUpdateById : Specification<Account>
{
    public WitnessAccountToUpdateById(AccountId accountId)
        : base(e => e.Id.Equals(accountId))
    {
        AddInclude(q => q.Include(e => e.Witness!));
        AddInclude(q => q.Include(e => e.Person!));
    }
}
