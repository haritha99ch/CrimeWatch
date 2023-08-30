using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
public sealed record GetAllReportsQuery : IRequest<List<Report>>;
