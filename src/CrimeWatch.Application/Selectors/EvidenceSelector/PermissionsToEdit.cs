namespace CrimeWatch.Application.Selectors.EvidenceSelector;
public record PermissionsToEdit(WitnessId WitnessId) : EntitySelector<Evidence, PermissionsToEdit>
{
    protected override Expression<Func<Evidence, PermissionsToEdit>> MapSelector()
        => e => new(e.WitnessId);
}
