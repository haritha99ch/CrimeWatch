namespace Application.Features.Reports.Commands.ModerateReport;
internal sealed class ModerateReportCommandHandler : ICommandHandler<ModerateReportCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public ModerateReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(ModerateReportCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking().GetByIdAsync(request.ReportId, cancellationToken);
        report!.SetModerator(request.AccountId);
        report = await _reportRepository.UpdateAsync(report, cancellationToken);
        return report.Status.Equals(Status.UnderReview) && report.ModeratorId!.Equals(request.AccountId);
    }
}
