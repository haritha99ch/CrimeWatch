using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.ReportQueries.GetReports;
public sealed record GetWitnessReportQuery(WitnessId WitnessId) : IRequest<List<Report>>;
