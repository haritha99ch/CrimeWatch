using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Helpers;
internal static class ModeratorRepositorySpecificationExtension
{
    public static async Task<Moderator?> GetModeratorWithAllByIdAsync(this IRepository<Moderator, ModeratorId> repository, ModeratorId id, CancellationToken cancellationToken)
        => await repository.GetByAsync<ModeratorWithAllById>(new(id), cancellationToken);
}
