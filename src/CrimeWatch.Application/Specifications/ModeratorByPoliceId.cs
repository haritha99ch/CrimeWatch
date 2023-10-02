using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Specifications;
public class ModeratorByPoliceId : Specification<Moderator, ModeratorId>
{
    public ModeratorByPoliceId(string policeId) : base(e => e.PoliceId.Equals(policeId)) { }
}
