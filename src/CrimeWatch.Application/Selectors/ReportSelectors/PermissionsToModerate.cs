namespace CrimeWatch.Application.Selectors.ReportSelectors;
public record PermissionsToModerate(ModeratorId? ModeratorId, Status Status)
    : EntitySelector<Report, PermissionsToModerate>
{
    protected override Expression<Func<Report, PermissionsToModerate>> MapSelector()
        => e => new(e.ModeratorId, e.Status);
}
