namespace CrimeWatch.Application.Specifications;
internal class ReportWithMediaItemById : Specification<Report>
{
    public ReportWithMediaItemById(ReportId id) : base(e => e.Id.Equals(id))
    {
        AddInclude(e => e.Include(e => e.MediaItem));
    }
}
