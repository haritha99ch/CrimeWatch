using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.RevertReportToReview;
public sealed record RevertReportToReviewCommand(ReportId ReportId) : IRequest<Report>;
