using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
internal class GetAllModeratedReportsCommandHandler : IRequestHandler<GetAllModeratedReportsCommand, List<Report>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetAllModeratedReportsCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<List<Report>> Handle(GetAllModeratedReportsCommand request, CancellationToken cancellationToken)
        => await _reportRepository.GetAllModeratedReportsAsync(cancellationToken);
}
