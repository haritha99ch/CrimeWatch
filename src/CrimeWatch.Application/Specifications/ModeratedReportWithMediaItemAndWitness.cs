using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Specifications;
internal class ModeratedReportWithMediaItemAndWitness : Specification<Report, ReportId>
{
    public ModeratedReportWithMediaItemAndWitness()
        : base(e => !(e.Status.Equals(Status.Pending) || e.Status.Equals(Status.UnderReview)))
    {
        AddInclude(e => e.Include(e => e.MediaItem));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.User));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.Account));
    }
}