using Application.Specifications.Reports;

namespace Application.Helpers.Repositories;
public static class ReportRepositoryExtension
{
    public async static Task<ReportDetails?> GetReportDetailsById(
            this IRepository<Report, ReportId> reportRepository,
            ReportId reportId,
            CancellationToken cancellationToken = default
        ) => await reportRepository.GetOneAsync<ReportDetailsById, ReportDetails>(new(reportId), cancellationToken);
}
