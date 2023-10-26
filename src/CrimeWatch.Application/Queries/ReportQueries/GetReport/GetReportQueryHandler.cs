namespace CrimeWatch.Application.Queries.ReportQueries.GetReport;
internal class GetReportQueryHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<GetReportQuery, Report>
{

    public async Task<Report> Handle(GetReportQuery request, CancellationToken cancellationToken)
        => await reportRepository.GetReportWithAllById(request.ReportId, cancellationToken)
            ?? throw new("Report not found");
}
