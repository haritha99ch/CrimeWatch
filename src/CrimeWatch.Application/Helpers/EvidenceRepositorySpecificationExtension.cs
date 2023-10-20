using CrimeWatch.Application.Selectors.EvidenceSelector;

namespace CrimeWatch.Application.Helpers;
internal static class EvidenceRepositorySpecificationExtension
{
    public static async Task<Evidence?> GetEvidenceWithMediaItemsByIdAsync(
        this IRepository<Evidence, EvidenceId> repository, EvidenceId id, CancellationToken cancellationToken)
        => await repository.GetOneAsync<EvidenceWithMediaItemsById>(new(id), cancellationToken);

    public static async Task<List<Evidence>> GetEvidencesWithAllForReportAsync(
        this IRepository<Evidence, EvidenceId> repository, ReportId reportId, CancellationToken cancellationToken)
        => await repository.GetManyAsync<EvidenceWithAllForReport>(new(reportId), cancellationToken);

    public static async Task<List<Evidence>> GetModeratedEvidencesWithAllForReportAsync(
        this IRepository<Evidence, EvidenceId> repository, ReportId reportId, CancellationToken cancellationToken)
        => await repository.GetManyAsync<ModeratedEvidenceWithAllForReport>(new(reportId), cancellationToken);

    public static async Task<bool> HasPermissionsToEditAsync(this IRepository<Evidence, EvidenceId> repository,
        EvidenceId evidenceId, WitnessId witnessId, CancellationToken cancellationToken)
    {
        var report = await repository.GetByIdAsync(evidenceId, PermissionsToEdit.Selector, cancellationToken);

        return report != null && witnessId.Equals(report.WitnessId);
    }

    public static async Task<bool> HasPermissionsToModerateAsync(
        this IRepository<Evidence, EvidenceId> reportRepository,
        EvidenceId evidenceId, ModeratorId? moderatorId, CancellationToken cancellationToken)
    {
        var report = await reportRepository.GetByIdAsync(evidenceId, PermissionsToModerate.Selector,
            cancellationToken);
        if (report is null) return false;
        if (report.Status.Equals(Status.Pending)) return true;
        return report.ModeratorId is null || report.ModeratorId.Equals(moderatorId);
    }
}
