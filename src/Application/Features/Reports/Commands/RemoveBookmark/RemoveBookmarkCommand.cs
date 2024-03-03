namespace Application.Features.Reports.Commands.RemoveBookmark;
public sealed record RemoveBookmarkCommand(ReportId ReportId, AccountId AccountId) : ICommand<bool>;
