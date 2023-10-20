namespace CrimeWatch.Application.Specifications;
internal class EvidenceWithAllForReport : Specification<Evidence, EvidenceId>
{
    public EvidenceWithAllForReport(ReportId reportId)
        : base(e => e.ReportId.Equals(reportId))
    {
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.User)
            .Include(e => e.Witness).ThenInclude(e => e!.Account));

        AddInclude(e => e.Include(e => e.Moderator).ThenInclude(e => e!.User)
            .Include(e => e.Moderator).ThenInclude(e => e!.Account));

        AddInclude(e => e.Include(e => e.MediaItems));
    }
}
