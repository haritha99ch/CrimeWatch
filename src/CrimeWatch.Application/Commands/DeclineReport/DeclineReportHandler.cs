using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.DeclineReport;
internal class DeclineReportHandler : IRequestHandler<DeclineReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public DeclineReportHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(DeclineReportCommand request, CancellationToken cancellationToken)
    {
        Report report =
            await _reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new Exception($"Report with id {request.ReportId} not found.");

        report.Decline();

        return await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}
