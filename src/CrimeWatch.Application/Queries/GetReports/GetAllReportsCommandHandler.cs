using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReports;
internal class GetAllReportsCommandHandler : IRequestHandler<GetAllReportsCommand, List<Report>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetAllReportsCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<List<Report>> Handle(GetAllReportsCommand request, CancellationToken cancellationToken)
        => await _reportRepository.GetAllReportsWithMediaItemAndWitnessByAsync(cancellationToken);
}
