namespace CrimeWatch.Application.Commands.ReportCommands.DeclineReport;
internal class DeclineReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<DeclineReportCommand, Report>
{

    public async Task<Report> Handle(DeclineReportCommand request, CancellationToken cancellationToken)
    {
        var report =
            await reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new($"Report with id {request.ReportId} not found.");

        report.Decline();

        return await reportRepository.UpdateAsync(report, cancellationToken);
    }
}
