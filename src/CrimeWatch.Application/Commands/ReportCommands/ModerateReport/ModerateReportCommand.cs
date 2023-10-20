namespace CrimeWatch.Application.Commands.ReportCommands.ModerateReport;
public sealed record ModerateReportCommand(ReportId ReportId, ModeratorId ModeratorId) : IRequest<Report>;
