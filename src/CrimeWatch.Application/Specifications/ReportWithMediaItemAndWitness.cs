using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Specifications;
internal class ReportWithMediaItemAndWitness : Specification<Report, ReportId>
{
    public ReportWithMediaItemAndWitness(ReportId? reportId = null)
        : base(e => reportId == null || e.Id.Equals(reportId))
    {
        AddInclude(e => e.Include(e => e.MediaItem));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.User));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.Account));
    }
}