using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.Accounts;
internal sealed class ModeratorAccountToUpdateById : Specification<Account>
{
    internal ModeratorAccountToUpdateById(AccountId accountId)
        : base(e => e.Id.Equals(accountId))
    {
        AddInclude(q => q.Include(e => e.Moderator!));
        AddInclude(q => q.Include(e => e.Person!));
    }
}
