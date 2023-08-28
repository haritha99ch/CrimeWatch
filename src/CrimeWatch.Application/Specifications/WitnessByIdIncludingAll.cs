using CrimeWatch.Domain.AggregateModels.WitnessAggregate;
using CrimeWatch.Infrastructure.Primitives;
using Microsoft.EntityFrameworkCore;

namespace CrimeWatch.Application.Specifications;
internal class WitnessByIdIncludingAll : Specification<Witness, WitnessId>
{
    public WitnessByIdIncludingAll(WitnessId witnessId) : base(e => e.Id.Equals(witnessId))
    {
        AddInclude(e => e.Include(e => e.User));
        AddInclude(e => e.Include(e => e.Account));
    }
}
