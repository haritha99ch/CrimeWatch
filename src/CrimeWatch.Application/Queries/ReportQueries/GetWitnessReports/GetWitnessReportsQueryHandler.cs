namespace CrimeWatch.Application.Queries.ReportQueries.GetWitnessReports;
internal class GetWitnessReportsQueryHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<GetWitnessReportsQuery, List<Report>>
{

    public async Task<List<Report>> Handle(GetWitnessReportsQuery request, CancellationToken cancellationToken)
        => await reportRepository.GetAllWitnessReportsAsync(request.WitnessId, cancellationToken);
}
