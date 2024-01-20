using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.AccountSpecifications;
internal sealed record WitnessAccountToUpdateById : Specification<Account>
{
    internal WitnessAccountToUpdateById(AccountId accountId)
        : base(e => e.Id.Equals(accountId))
    {
        AddInclude(q => q.Include(e => e.Witness!));
        AddInclude(q => q.Include(e => e.Person!));
    }
}
