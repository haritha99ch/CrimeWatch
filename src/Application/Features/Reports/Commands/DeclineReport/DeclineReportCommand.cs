namespace Application.Features.Reports.Commands.DeclineReport;
public sealed record DeclineReportCommand(ReportId ReportId) : ICommand<bool>;
