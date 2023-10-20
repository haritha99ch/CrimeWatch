namespace CrimeWatch.Application.Specifications;
internal class ModeratedEvidenceWithAllForReport : Specification<Evidence, EvidenceId>
{
    public ModeratedEvidenceWithAllForReport(ReportId reportId)
        : base(e => e.ReportId.Equals(reportId)
            && !(e.Status.Equals(Status.Pending) || e.Status.Equals(Status.UnderReview)))
    {
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.User)
            .Include(e => e.Witness).ThenInclude(e => e!.Account));

        AddInclude(e => e.Include(e => e.Moderator).ThenInclude(e => e!.User)
            .Include(e => e.Moderator).ThenInclude(e => e!.Account));

        AddInclude(e => e.Include(e => e.MediaItems));
    }
}
