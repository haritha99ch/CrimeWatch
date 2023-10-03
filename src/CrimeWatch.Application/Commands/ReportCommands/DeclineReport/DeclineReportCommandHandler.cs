using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ReportCommands.DeclineReport;
internal class DeclineReportCommandHandler : IRequestHandler<DeclineReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public DeclineReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(DeclineReportCommand request, CancellationToken cancellationToken)
    {
        var report =
            await _reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new($"Report with id {request.ReportId} not found.");

        report.Decline();

        return await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}
