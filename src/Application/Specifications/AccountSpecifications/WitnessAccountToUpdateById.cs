using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.AccountSpecifications;
internal sealed record WitnessAccountUpdateInfo : Specification<Account>
{
    internal WitnessAccountUpdateInfo(AccountId accountId)
        : base(e => e.Id.Equals(accountId))
    {
        AddInclude(q => q.Include(e => e.Witness!));
        AddInclude(q => q.Include(e => e.Person!));
    }
}
