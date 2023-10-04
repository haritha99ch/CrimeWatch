using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.ReportQueries.GetAllModeratedReports;
public record GetAllModeratedReportsQuery : IRequest<List<Report>>;
