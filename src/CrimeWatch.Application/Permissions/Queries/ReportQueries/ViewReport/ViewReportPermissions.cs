namespace CrimeWatch.Application.Permissions.Queries.ReportQueries.ViewReport;
public sealed partial record GetReportPermissions(ReportId? ReportId = null) : IRequest<ReportPermissions>;
