namespace CrimeWatch.Application.Commands.ReportCommands.ApproveReport;
internal class ApproveReportCommandHandler : IRequestHandler<ApproveReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public ApproveReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(ApproveReportCommand request, CancellationToken cancellationToken)
    {
        var report =
            await _reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new($"Report with id {request.ReportId} not found.");

        report.Approve();

        return await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}
