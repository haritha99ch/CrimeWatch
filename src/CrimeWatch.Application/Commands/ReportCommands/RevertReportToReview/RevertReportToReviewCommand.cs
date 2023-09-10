using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ReportCommands.RevertReportToReview;
public sealed record RevertReportToReviewCommand(ReportId ReportId) : IRequest<Report>;
