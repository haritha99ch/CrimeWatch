namespace CrimeWatch.Application.Selectors.ReportSelectors;
public record PermissionsToEdit(WitnessId WitnessId) : EntitySelector<Report, PermissionsToEdit>
{
    protected override Expression<Func<Report, PermissionsToEdit>> MapSelector()
        => e => new(e.WitnessId);
}
