using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Specifications;
internal class EvidenceByIdWithMediaItems : Specification<Evidence, EvidenceId>
{
    public EvidenceByIdWithMediaItems(EvidenceId id) : base(e => e.Id.Equals(id))
    {
        AddInclude(e => e.Include(e => e.MediaItems));
    }
}
