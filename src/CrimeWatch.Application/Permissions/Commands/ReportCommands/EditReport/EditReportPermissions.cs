namespace CrimeWatch.Application.Permissions.Commands.ReportCommands.EditReport;
public sealed record EditReportPermissions(ReportId ReportId) : IRequest<ReportPermissions>;