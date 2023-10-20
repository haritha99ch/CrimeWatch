namespace CrimeWatch.Application.Commands.ReportCommands.AddCommentToReport;
internal class AddCommentToReportCommandHandler : IRequestHandler<AddCommentToReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public AddCommentToReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(AddCommentToReportCommand request, CancellationToken cancellationToken)
    {
        var report =
            await _reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new($"Report with id {request.ReportId} not found.");

        report.Comment(request.Comment);

        return await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}
