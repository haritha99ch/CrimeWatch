using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
public sealed record GetModeratorReportsCommand(ModeratorId ModeratorId) : IRequest<List<Report>>;
