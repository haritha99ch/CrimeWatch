namespace CrimeWatch.Application.Commands.ReportCommands.RevertReportToReview;
public sealed record RevertReportToReviewCommand(ReportId ReportId) : IRequest<Report>;
