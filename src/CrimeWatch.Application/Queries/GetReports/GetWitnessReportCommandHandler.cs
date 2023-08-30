using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
internal class GetWitnessReportCommandHandler : IRequestHandler<GetWitnessReportCommand, List<Report>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetWitnessReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<List<Report>> Handle(GetWitnessReportCommand request, CancellationToken cancellationToken)
        => await _reportRepository.GetAllWitnessReportsAsync(request.WitnessId, cancellationToken);
}
