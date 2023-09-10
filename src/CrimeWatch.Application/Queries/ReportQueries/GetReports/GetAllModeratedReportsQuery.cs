using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.ReportQueries.GetReports;
public sealed record GetAllModeratedReportsQuery : IRequest<List<Report>>;
