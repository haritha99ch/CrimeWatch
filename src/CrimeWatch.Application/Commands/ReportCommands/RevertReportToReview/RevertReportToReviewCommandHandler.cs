namespace CrimeWatch.Application.Commands.ReportCommands.RevertReportToReview;
internal class RevertReportToReviewCommandHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<RevertReportToReviewCommand, Report>
{

    public async Task<Report> Handle(RevertReportToReviewCommand request, CancellationToken cancellationToken)
    {
        var report =
            await reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new($"Report with id {request.ReportId} not found.");

        report.RevertToReview();

        return await reportRepository.UpdateAsync(report, cancellationToken);
    }
}
