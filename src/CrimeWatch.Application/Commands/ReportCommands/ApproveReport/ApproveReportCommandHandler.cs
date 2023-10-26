namespace CrimeWatch.Application.Commands.ReportCommands.ApproveReport;
internal class ApproveReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<ApproveReportCommand, Report>
{

    public async Task<Report> Handle(ApproveReportCommand request, CancellationToken cancellationToken)
    {
        var report =
            await reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new($"Report with id {request.ReportId} not found.");

        report.Approve();

        return await reportRepository.UpdateAsync(report, cancellationToken);
    }
}
