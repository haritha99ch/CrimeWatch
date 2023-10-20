namespace CrimeWatch.Application.Specifications;
internal class EvidenceWithMediaItemsById : Specification<Evidence, EvidenceId>
{
    public EvidenceWithMediaItemsById(EvidenceId id) : base(e => e.Id.Equals(id))
    {
        AddInclude(e => e.Include(e => e.MediaItems));
    }
}
