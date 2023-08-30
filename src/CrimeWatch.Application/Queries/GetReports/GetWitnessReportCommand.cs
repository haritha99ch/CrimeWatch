using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
public sealed record GetWitnessReportCommand(WitnessId WitnessId) : IRequest<List<Report>>;
