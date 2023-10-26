namespace CrimeWatch.Application.Commands.ReportCommands.AddCommentToReport;
internal class AddCommentToReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<AddCommentToReportCommand, Report>
{

    public async Task<Report> Handle(AddCommentToReportCommand request, CancellationToken cancellationToken)
    {
        var report =
            await reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new($"Report with id {request.ReportId} not found.");

        report.Comment(request.Comment);

        return await reportRepository.UpdateAsync(report, cancellationToken);
    }
}
