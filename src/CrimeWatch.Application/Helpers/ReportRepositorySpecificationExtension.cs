﻿using CrimeWatch.Application.Selectors.ReportSelectors;

namespace CrimeWatch.Application.Helpers;
internal static class ReportRepositorySpecificationExtension
{
    public static async Task<List<Report>> GetAllWitnessReportsAsync(this IRepository<Report, ReportId> repository,
        WitnessId witnessId, CancellationToken cancellationToken)
        => await repository.GetManyAsync<WitnessReportWithMediaItemAndWitness>(new(witnessId), cancellationToken);

    public static async Task<List<Report>> GetAllModeratedReportsAsync(this IRepository<Report, ReportId> repository,
        CancellationToken cancellationToken)
        => await repository.GetManyAsync<ModeratedReportWithMediaItemModeratorAndWitness>(new(), cancellationToken);

    public static async Task<List<Report>> GetAllReportsWithMediaItemModeratorAndWitnessByAsync(
        this IRepository<Report, ReportId> repository, CancellationToken cancellationToken)
        => await repository.GetAllAsync<ReportWithMediaItemModeratorAndWitness>(cancellationToken);

    public static async Task<Report?> ReportWithMediaItemAndWitnessByIdAsync(
        this IRepository<Report, ReportId> repository, ReportId reportId, CancellationToken cancellationToken)
        => await repository.GetOneAsync<ReportWithMediaItemModeratorAndWitness>(new(reportId), cancellationToken);

    public static async Task<Report?> GetReportWithMediaItemByIdAsync(this IRepository<Report, ReportId> repository,
        ReportId reportId, CancellationToken cancellationToken)
        => await repository.GetOneAsync<ReportWithMediaItemById>(new(reportId), cancellationToken);

    public static async Task<List<Report>> GetAllModeratorReportsAsync(this IRepository<Report, ReportId> repository,
        ModeratorId moderatorId, CancellationToken cancellationToken)
        => await repository.GetManyAsync<ModeratorReportWithMediaItemModeratorAndWitness>(new(moderatorId),
            cancellationToken);

    public static async Task<Report?> GetReportWithAllById(this IRepository<Report, ReportId> repository,
        ReportId reportId, CancellationToken cancellationToken)
        => await repository.GetOneAsync<ReportWithAllById>(new(reportId), cancellationToken);

    public static async Task<bool> HasPermissionsToEditAsync(this IRepository<Report, ReportId> repository,
        ReportId reportId, WitnessId witnessId, CancellationToken cancellationToken)
    {
        var report = await repository.GetByIdAsync(reportId, PermissionsToEdit.Selector, cancellationToken);

        return report != null && witnessId.Equals(report.WitnessId);
    }

    public static async Task<bool> HasPermissionsToModerateAsync(this IRepository<Report, ReportId> reportRepository,
        ReportId reportId, ModeratorId? moderatorId, CancellationToken cancellationToken)
    {
        var report = await reportRepository.GetByIdAsync(reportId, PermissionsToModerate.Selector,
            cancellationToken);
        if (report is null) return false;
        if (report.Status.Equals(Status.Pending)) return true;
        return report.ModeratorId is null || report.ModeratorId.Equals(moderatorId);
    }
}
