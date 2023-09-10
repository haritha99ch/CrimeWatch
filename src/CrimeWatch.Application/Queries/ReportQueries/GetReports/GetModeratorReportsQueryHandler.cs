using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.ReportQueries.GetReports;
internal class GetModeratorReportsQueryHandler : IRequestHandler<GetModeratorReportsQuery, List<Report>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetModeratorReportsQueryHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public Task<List<Report>> Handle(GetModeratorReportsQuery request, CancellationToken cancellationToken)
        => _reportRepository.GetAllModeratorReportsAsync(request.ModeratorId, cancellationToken);
}
