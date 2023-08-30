using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
public sealed record GetWitnessReportQuery(WitnessId WitnessId) : IRequest<List<Report>>;
