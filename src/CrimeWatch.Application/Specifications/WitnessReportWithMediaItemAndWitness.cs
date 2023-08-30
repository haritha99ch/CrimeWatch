using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Specifications;

internal class WitnessReportWithMediaItemAndWitness : Specification<Report, ReportId>
{
    public WitnessReportWithMediaItemAndWitness(WitnessId witnessId)
        : base(e => e.WitnessId.Equals(witnessId))
    {
        AddInclude(e => e.Include(e => e.MediaItem));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.User));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.Account));
    }
}