using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.DeclineReport;
public sealed record DeclineReportCommand
    (ReportId ReportId) : IRequest<Report>;
