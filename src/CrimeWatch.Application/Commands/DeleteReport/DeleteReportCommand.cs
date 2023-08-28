namespace CrimeWatch.Application.Commands.DeleteReport;
public sealed record DeleteReportCommand(ReportId Id) : IRequest<bool>;
