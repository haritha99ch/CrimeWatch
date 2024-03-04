namespace Application.Features.Reports.Commands.DeclineReport;
internal sealed class DeclineReportCommandHandler : ICommandHandler<DeclineReportCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public DeclineReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(DeclineReportCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking().GetByIdAsync(request.ReportId, cancellationToken);
        report!.SetDeclined();
        report = await _reportRepository.UpdateAsync(report, cancellationToken);
        return report.Status.Equals(Status.Declined);
    }
}
