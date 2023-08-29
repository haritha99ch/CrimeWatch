using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ModerateReport;
public sealed record ModerateReportCommand
    (ReportId ReportId, ModeratorId ModeratorId) : IRequest<Report>;
