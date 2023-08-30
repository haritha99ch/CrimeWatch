using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
internal class GetWitnessReportQueryHandler : IRequestHandler<GetWitnessReportQuery, List<Report>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetWitnessReportQueryHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<List<Report>> Handle(GetWitnessReportQuery request, CancellationToken cancellationToken)
        => await _reportRepository.GetAllWitnessReportsAsync(request.WitnessId, cancellationToken);
}
