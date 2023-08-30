using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Specifications;
internal class WitnessWithAll : Specification<Witness, WitnessId>
{
    public WitnessWithAll(WitnessId witnessId) : base(e => e.Id.Equals(witnessId))
        => AddIncludes();

    public WitnessWithAll(AccountId accountId) : base(e => e.AccountId.Equals(accountId))
        => AddIncludes();

    private void AddIncludes()
    {
        AddInclude(e => e.Include(e => e.User));
        AddInclude(e => e.Include(e => e.Account));
    }
}
