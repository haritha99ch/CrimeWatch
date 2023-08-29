using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.AddCommentToReport;
public sealed record AddCommentToReportCommand
    (ReportId ReportId, string Comment) : IRequest<Report>;
