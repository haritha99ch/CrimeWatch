using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.AccountSpecifications;
internal sealed record ModeratorAccountUpdateInfo : Specification<Account>
{
    internal ModeratorAccountUpdateInfo(AccountId accountId)
        : base(e => e.Id.Equals(accountId))
    {
        AddInclude(q => q.Include(e => e.Moderator!));
        AddInclude(q => q.Include(e => e.Person!));
    }
}
