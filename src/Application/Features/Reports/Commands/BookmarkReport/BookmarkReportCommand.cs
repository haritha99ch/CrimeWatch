namespace Application.Features.Reports.Commands.BookmarkReport;
public sealed record BookmarkReportCommand(ReportId ReportId, AccountId AccountId) : ICommand<bool>;
