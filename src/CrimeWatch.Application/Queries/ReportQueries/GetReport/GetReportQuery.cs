namespace CrimeWatch.Application.Queries.ReportQueries.GetReport;
public sealed record GetReportQuery(ReportId ReportId) : IRequest<Report>;
