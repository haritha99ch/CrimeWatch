using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
public sealed record GetAllModeratedReportsQuery : IRequest<List<Report>>;
