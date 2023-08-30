using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Helpers;
internal static class ReportRepositorySpecificationExtension
{
    public static async Task<List<Report>> GetAllWitnessReportsAsync(this IRepository<Report, ReportId> repository, WitnessId witnessId, CancellationToken cancellationToken)
        => await repository.GetAllByAsync<WitnessReportWithMediaItemAndWitness>(new(witnessId), cancellationToken);

    public static async Task<List<Report>> GetAllModeratedReportsAsync(this IRepository<Report, ReportId> repository, CancellationToken cancellationToken)
        => await repository.GetAllByAsync<ModeratedReportWithMediaItemAndWitness>(new(), cancellationToken);

    public static async Task<List<Report>> GetAllReportsWithMediaItemAndWitnessByAsync(this IRepository<Report, ReportId> repository, CancellationToken cancellationToken)
        => await repository.GetAllByAsync<ReportWithMediaItemAndWitness>(new(), cancellationToken);

    public static async Task<Report?> ReportWithMediaItemAndWitnessByIdAsync(this IRepository<Report, ReportId> repository, ReportId reportId, CancellationToken cancellationToken)
        => await repository.GetByAsync<ReportWithMediaItemAndWitness>(new(reportId), cancellationToken);

    public static async Task<Report?> GetReportWithMediaItemByIdAsync(this IRepository<Report, ReportId> repository, ReportId reportId, CancellationToken cancellationToken)
        => await repository.GetByAsync<ReportWithMediaItemById>(new(reportId), cancellationToken);
}
