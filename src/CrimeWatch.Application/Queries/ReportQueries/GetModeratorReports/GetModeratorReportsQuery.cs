namespace CrimeWatch.Application.Queries.ReportQueries.GetModeratorReports;
public record GetModeratorReportsQuery(ModeratorId ModeratorId) : IRequest<List<Report>>;
