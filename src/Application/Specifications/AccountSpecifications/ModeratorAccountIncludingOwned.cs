using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.AccountSpecifications;
sealed internal record ModeratorAccountIncludingOwned : Specification<Account>
{
    internal ModeratorAccountIncludingOwned(AccountId accountId)
        : base(e => e.Id.Equals(accountId))
    {
        AddInclude(q => q.Include(e => e.Moderator!));
        AddInclude(q => q.Include(e => e.Person!));
    }
}
