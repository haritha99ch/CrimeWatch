using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Helpers;
internal static class EvidenceRepositorySpecificationExtension
{
    public static async Task<Evidence?> GetEvidenceWithMediaItemsByIdAsync(this IRepository<Evidence, EvidenceId> repository, EvidenceId id, CancellationToken cancellationToken)
        => await repository.GetByAsync<EvidenceWithMediaItemsById>(new(id), cancellationToken);
}
