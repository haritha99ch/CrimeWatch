namespace CrimeWatch.Application.Queries.ReportQueries.GetReport;
internal class GetReportQueryHandler : IRequestHandler<GetReportQuery, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetReportQueryHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(GetReportQuery request, CancellationToken cancellationToken)
        => await _reportRepository.GetReportWithAllById(request.ReportId, cancellationToken)
            ?? throw new("Report not found");
}
