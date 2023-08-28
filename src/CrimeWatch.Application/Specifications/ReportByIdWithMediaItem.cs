using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Specifications;
internal class ReportByIdWithMediaItem : Specification<Report, ReportId>
{
    public ReportByIdWithMediaItem(ReportId id) : base(e => e.Id.Equals(id))
    {
        AddInclude(e => e.Include(e => e.MediaItem));
    }
}
