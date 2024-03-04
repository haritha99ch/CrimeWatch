namespace Application.Features.Reports.Commands.ApproveReport;
internal sealed class ApproveReportCommandHandler : ICommandHandler<ApproveReportCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public ApproveReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(ApproveReportCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking().GetByIdAsync(request.ReportId, cancellationToken);
        report!.SetApproved();
        report = await _reportRepository.UpdateAsync(report, cancellationToken);
        return report.Status.Equals(Status.Approved);
    }
}
