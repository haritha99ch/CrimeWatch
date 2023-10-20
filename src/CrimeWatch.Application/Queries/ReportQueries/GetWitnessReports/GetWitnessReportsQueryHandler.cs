namespace CrimeWatch.Application.Queries.ReportQueries.GetWitnessReports;
internal class GetWitnessReportsQueryHandler : IRequestHandler<GetWitnessReportsQuery, List<Report>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetWitnessReportsQueryHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<List<Report>> Handle(GetWitnessReportsQuery request, CancellationToken cancellationToken)
        => await _reportRepository.GetAllWitnessReportsAsync(request.WitnessId, cancellationToken);
}
