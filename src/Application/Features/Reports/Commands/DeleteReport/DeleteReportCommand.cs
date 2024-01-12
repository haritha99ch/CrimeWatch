namespace Application.Features.Reports.Commands.DeleteReport;
public sealed record DeleteReportCommand(ReportId ReportId) : ICommand<bool>;
