namespace CrimeWatch.Application.Selectors.EvidenceSelector;
public record PermissionsToModerate
    (ModeratorId? ModeratorId, Status Status) : EntitySelector<Evidence, PermissionsToModerate>
{
    protected override Expression<Func<Evidence, PermissionsToModerate>> MapSelector()
        => e => new(e.ModeratorId, e.Status);
}
