using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Helpers;
internal static class ModeratorRepositorySpecificationExtension
{
    public static async Task<Moderator?> GetModeratorWithAllByIdAsync(this IRepository<Moderator, ModeratorId> repository, ModeratorId id, CancellationToken cancellationToken)
        => await repository.GetByAsync<ModeratorWithAll>(new(id), cancellationToken);

    public static async Task<Moderator?> GetModeratorWithAllByAccountIdAsync(this IRepository<Moderator, ModeratorId> repository, AccountId accountId, CancellationToken cancellationToken)
        => await repository.GetByAsync<ModeratorWithAll>(new(accountId), cancellationToken);
}
