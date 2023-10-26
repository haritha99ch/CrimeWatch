namespace CrimeWatch.Application.Queries.ReportQueries.GetAllReports;
internal class GetAllReportsQueryHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<GetAllReportsQuery, List<Report>>
{

    public async Task<List<Report>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        => await reportRepository.GetAllAsync(cancellationToken);
}
