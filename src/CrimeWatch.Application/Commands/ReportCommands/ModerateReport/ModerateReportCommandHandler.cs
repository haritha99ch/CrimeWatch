namespace CrimeWatch.Application.Commands.ReportCommands.ModerateReport;
internal class ModerateReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<ModerateReportCommand, Report>
{

    public async Task<Report> Handle(ModerateReportCommand request, CancellationToken cancellationToken)
    {
        var report =
            await reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new($"Report with id {request.ReportId} not found.");

        report.Moderate(request.ModeratorId);

        return await reportRepository.UpdateAsync(report, cancellationToken);
    }
}
