namespace Application.Features.Reports.Commands.RevertReportToUnderReview;
public sealed record RevertReportToUnderReviewCommand(ReportId ReportId) : ICommand<bool>;
