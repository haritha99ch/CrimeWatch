using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Specifications;
internal class ReportWithMediaItemModeratorAndWitness : Specification<Report, ReportId>
{
    public ReportWithMediaItemModeratorAndWitness(ReportId? reportId = null)
        : base(e => reportId == null || e.Id.Equals(reportId))
    {
        AddInclude(e => e.Include(e => e.MediaItem));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.User));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.Account));
        AddInclude(e => e.Include(e => e.Moderator).ThenInclude(e => e!.User));
    }
}