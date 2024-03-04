namespace Application.Features.Reports.Commands.ApproveReport;
public sealed record ApproveReportCommand(ReportId ReportId) : ICommand<bool>;
