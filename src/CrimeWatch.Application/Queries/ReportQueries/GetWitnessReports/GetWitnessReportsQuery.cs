namespace CrimeWatch.Application.Queries.ReportQueries.GetWitnessReports;
public record GetWitnessReportsQuery(WitnessId WitnessId) : IRequest<List<Report>>;
