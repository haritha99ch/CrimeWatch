namespace Application.Features.Reports.Commands.ModerateReport;
public record ModerateReportCommand(ReportId ReportId, AccountId AccountId) : ICommand<bool>;
