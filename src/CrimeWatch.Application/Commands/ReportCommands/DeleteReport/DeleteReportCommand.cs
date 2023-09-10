namespace CrimeWatch.Application.Commands.ReportCommands.DeleteReport;
public sealed record DeleteReportCommand(ReportId Id) : IRequest<bool>;
