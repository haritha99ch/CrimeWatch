using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Helpers;
internal static class EvidenceRepositorySpecificationExtension
{
    public static async Task<Evidence?> GetEvidenceWithMediaItemsByIdAsync(this IRepository<Evidence, EvidenceId> repository, EvidenceId id, CancellationToken cancellationToken)
        => await repository.GetByAsync<EvidenceWithMediaItemsById>(new(id), cancellationToken);

    public static async Task<List<Evidence>> GetEvidencesWithAllForReportAsync(this IRepository<Evidence, EvidenceId> repository, ReportId reportId, CancellationToken cancellationToken)
        => await repository.GetAllByAsync<EvidenceWithAllForReport>(new(reportId), cancellationToken);

    public static async Task<List<Evidence>> GetModeratedEvidencesWithAllForReportAsync(this IRepository<Evidence, EvidenceId> repository, ReportId reportId, CancellationToken cancellationToken)
        => await repository.GetAllByAsync<ModeratedEvidenceWithAllForReport>(new(reportId), cancellationToken);
}
