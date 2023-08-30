using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Helpers;
internal static class WitnessRepositorySpecificationExtension
{
    public static async Task<Witness?> GetWitnessWithAllByIdAsync(this IRepository<Witness, WitnessId> witnessRepository, WitnessId witnessId, CancellationToken cancellationToken)
        => await witnessRepository.GetByAsync<WitnessWithAll>(new(witnessId), cancellationToken);

    public static async Task<Witness?> GetWitnessWithAllByAccountIdAsync(this IRepository<Witness, WitnessId> witnessRepository, AccountId accountId, CancellationToken cancellationToken)
        => await witnessRepository.GetByAsync<WitnessWithAll>(new(accountId), cancellationToken);
}
