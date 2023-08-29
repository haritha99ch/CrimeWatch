using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ModerateReport;
internal class ModerateReportCommandHandler : IRequestHandler<ModerateReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public ModerateReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(ModerateReportCommand request, CancellationToken cancellationToken)
    {
        Report report =
            await _reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new Exception($"Report with id {request.ReportId} not found.");

        report.Moderate(request.ModeratorId);

        return await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}
