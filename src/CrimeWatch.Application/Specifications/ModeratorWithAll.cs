using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Specifications;
internal class ModeratorWithAll : Specification<Moderator, ModeratorId>
{
    public ModeratorWithAll(ModeratorId moderator) : base(e => e.Id.Equals(moderator))
        => AddIncludes();

    public ModeratorWithAll(AccountId accountId) : base(e => e.AccountId.Equals(accountId))
        => AddIncludes();

    private void AddIncludes()
    {
        AddInclude(e => e.Include(e => e.User));
        AddInclude(e => e.Include(e => e.Account));
    }
}