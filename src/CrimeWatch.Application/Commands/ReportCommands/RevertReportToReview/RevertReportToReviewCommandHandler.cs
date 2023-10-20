namespace CrimeWatch.Application.Commands.ReportCommands.RevertReportToReview;
internal class RevertReportToReviewCommandHandler : IRequestHandler<RevertReportToReviewCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public RevertReportToReviewCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(RevertReportToReviewCommand request, CancellationToken cancellationToken)
    {
        var report =
            await _reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new($"Report with id {request.ReportId} not found.");

        report.RevertToReview();

        return await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}
