using CrimeWatch.Domain.AggregateModels.WitnessAggregate;
using CrimeWatch.Infrastructure.Primitives;
using Microsoft.EntityFrameworkCore;

namespace CrimeWatch.Application.Specifications;
internal class GetWitnessByIdIncludingAll : Specification<Witness, WitnessId>
{
    public GetWitnessByIdIncludingAll(WitnessId witnessId) : base(e => e.Id.Equals(witnessId))
    {
        AddInclude(e => e.Include(e => e.User));
        AddInclude(e => e.Include(e => e.Account));
    }
}
