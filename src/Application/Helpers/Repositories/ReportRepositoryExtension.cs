namespace Application.Helpers.Repositories;
public static class ReportRepositoryExtension
{
    public async static Task<ReportDetails?> GetReportDetailsById(
            this IRepository<Report, ReportId> reportRepository,
            ReportId reportId,
            CancellationToken? cancellationToken = null
        ) => await reportRepository.GetByIdAsync(reportId, ReportDetails.SelectQueryable(), cancellationToken);
}
