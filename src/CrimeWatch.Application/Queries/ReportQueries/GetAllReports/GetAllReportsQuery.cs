using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.ReportQueries.GetAllReports;
public record GetAllReportsQuery : IRequest<List<Report>>;
