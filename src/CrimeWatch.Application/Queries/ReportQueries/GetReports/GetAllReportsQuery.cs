using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.ReportQueries.GetReports;
public sealed record GetAllReportsQuery : IRequest<List<Report>>;
