using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReport;
public sealed record GetReportQuery(ReportId ReportId) : IRequest<Report>;
