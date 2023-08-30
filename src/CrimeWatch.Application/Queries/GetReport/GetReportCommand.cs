using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReport;
public sealed record GetReportCommand(ReportId ReportId) : IRequest<Report>;
