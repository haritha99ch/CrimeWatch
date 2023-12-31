﻿using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Helpers;
internal static class ModeratorRepositorySpecificationExtension
{
    public static async Task<Moderator?> GetModeratorWithAllByIdAsync(
        this IRepository<Moderator, ModeratorId> repository, ModeratorId id, CancellationToken cancellationToken)
        => await repository.GetOneAsync<ModeratorWithAll>(new(id), cancellationToken);

    public static async Task<Moderator?> GetModeratorWithAllByAccountIdAsync(
        this IRepository<Moderator, ModeratorId> repository, AccountId accountId, CancellationToken cancellationToken)
        => await repository.GetOneAsync<ModeratorWithAll>(new(accountId), cancellationToken);

    public static async Task<bool> IsPoliceIdUniqueAsync(
        this IRepository<Moderator, ModeratorId> repository, string policeId,
        CancellationToken cancellationToken)
        => !await repository.ExistsAsync<ModeratorByPoliceId>(new(policeId), cancellationToken);
}
