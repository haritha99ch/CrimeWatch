using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Helpers;
internal static class WitnessRepositorySpecificationExtension
{
    public static async Task<Witness?> GetWitnessWithAllByIdAsync(this IRepository<Witness, WitnessId> witnessRepository, WitnessId witnessId, CancellationToken cancellationToken)
        => await witnessRepository.GetByAsync<WitnessWithAllById>(new(witnessId), cancellationToken);
}
