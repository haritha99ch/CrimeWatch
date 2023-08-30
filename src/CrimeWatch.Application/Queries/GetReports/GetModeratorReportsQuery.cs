using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
public sealed record GetModeratorReportsQuery(ModeratorId ModeratorId) : IRequest<List<Report>>;
