namespace CrimeWatch.Application.Specifications;
internal class ModeratorReportWithMediaItemModeratorAndWitness : Specification<Report, ReportId>
{
    public ModeratorReportWithMediaItemModeratorAndWitness(ModeratorId moderatorId)
        : base(r => r.ModeratorId == moderatorId)
    {
        AddInclude(e => e.Include(e => e.MediaItem));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.User));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.Account));
    }
}
