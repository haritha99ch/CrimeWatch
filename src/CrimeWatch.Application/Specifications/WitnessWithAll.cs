namespace CrimeWatch.Application.Specifications;
internal class WitnessWithAll : Specification<Witness>
{
    public WitnessWithAll(WitnessId witnessId) : base(e => e.Id.Equals(witnessId))
    {
        AddIncludes();
    }

    public WitnessWithAll(AccountId accountId) : base(e => e.AccountId.Equals(accountId))
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        AddInclude(e => e.Include(w => w.User));
        AddInclude(e => e.Include(w => w.Account));
    }
}
