using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetReport;
internal class GetReportCommandHandler : IRequestHandler<GetReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(GetReportCommand request, CancellationToken cancellationToken)
        => await _reportRepository.GetReportWithAllById(request.ReportId, cancellationToken)
        ?? throw new Exception("Report not found");
}
