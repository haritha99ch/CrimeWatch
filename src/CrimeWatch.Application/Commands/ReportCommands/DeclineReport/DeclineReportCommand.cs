namespace CrimeWatch.Application.Commands.ReportCommands.DeclineReport;
public sealed record DeclineReportCommand(ReportId ReportId) : IRequest<Report>;
