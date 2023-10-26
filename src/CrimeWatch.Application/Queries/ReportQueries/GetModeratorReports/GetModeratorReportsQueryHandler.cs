namespace CrimeWatch.Application.Queries.ReportQueries.GetModeratorReports;
internal class GetModeratorReportsQueryHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<GetModeratorReportsQuery, List<Report>>
{
    public Task<List<Report>> Handle(GetModeratorReportsQuery request, CancellationToken cancellationToken)
        => reportRepository.GetAllModeratorReportsAsync(request.ModeratorId, cancellationToken);
}
