using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
internal class GetModeratorReportsCommandHandler : IRequestHandler<GetModeratorReportsCommand, List<Report>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetModeratorReportsCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public Task<List<Report>> Handle(GetModeratorReportsCommand request, CancellationToken cancellationToken)
        => _reportRepository.GetAllModeratorReportsAsync(request.ModeratorId, cancellationToken);
}
