namespace CrimeWatch.Application.Specifications;
internal class ReportWithAllById : Specification<Report>
{
    public ReportWithAllById(ReportId reportId)
        : base(e => e.Id.Equals(reportId))
    {
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.User)
            .Include(e => e.Witness).ThenInclude(e => e!.Account));

        AddInclude(e => e.Include(e => e.Moderator).ThenInclude(e => e!.User)
            .Include(e => e.Moderator).ThenInclude(e => e!.Account));

        AddInclude(e => e.AsNoTracking().Include(e => e.MediaItem));
    }
}
