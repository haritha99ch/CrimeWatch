namespace Application.Features.Reports.Commands.RevertReportToUnderReview;
internal sealed class RevertReportToUnderReviewCommandHandler : ICommandHandler<RevertReportToUnderReviewCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public RevertReportToUnderReviewCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(
            RevertReportToUnderReviewCommand request,
            CancellationToken cancellationToken
        )
    {
        var report = await _reportRepository.AsTracking().GetByIdAsync(request.ReportId, cancellationToken);
        report!.SetUnderReview();
        report = await _reportRepository.UpdateAsync(report, cancellationToken);
        return report.Status.Equals(Status.UnderReview);
    }
}
