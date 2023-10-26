namespace CrimeWatch.Application.Specifications;
internal class ModeratedReportWithMediaItemModeratorAndWitness : Specification<Report>
{
    public ModeratedReportWithMediaItemModeratorAndWitness()
        : base(e => !(e.Status.Equals(Status.Pending) || e.Status.Equals(Status.UnderReview)))
    {
        AddInclude(e => e.Include(e => e.MediaItem));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.User));
        AddInclude(e => e.Include(e => e.Witness).ThenInclude(e => e!.Account));
        AddInclude(e => e.Include(e => e.Moderator).ThenInclude(e => e!.User));
    }
}
