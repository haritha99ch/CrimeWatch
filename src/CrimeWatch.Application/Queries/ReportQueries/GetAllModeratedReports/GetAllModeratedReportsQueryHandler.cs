namespace CrimeWatch.Application.Queries.ReportQueries.GetAllModeratedReports;
public class GetAllModeratedReportsQueryHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<GetAllModeratedReportsQuery, List<Report>>
{
    public async Task<List<Report>> Handle(GetAllModeratedReportsQuery request, CancellationToken cancellationToken)
        => await reportRepository.GetAllModeratedReportsAsync(cancellationToken);
}
