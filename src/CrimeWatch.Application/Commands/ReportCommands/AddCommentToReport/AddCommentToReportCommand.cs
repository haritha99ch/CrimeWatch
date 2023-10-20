namespace CrimeWatch.Application.Commands.ReportCommands.AddCommentToReport;
public sealed record AddCommentToReportCommand(ReportId ReportId, string Comment) : IRequest<Report>;
