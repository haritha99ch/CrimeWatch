using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Specifications;
internal class ModeratorReportWithMediaItemAndWitness : Specification<Report, ReportId>
{
    public ModeratorReportWithMediaItemAndWitness(ModeratorId moderatorId)
        : base(r => r.ModeratorId == moderatorId)
    {
        AddInclude(e => e.Include(e => e.MediaItem));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.User));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.Account));
    }
}