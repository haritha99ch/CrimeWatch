using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.AddCommentToReport;
internal class AddCommentToReportCommandHandler : IRequestHandler<AddCommentToReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public AddCommentToReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(AddCommentToReportCommand request, CancellationToken cancellationToken)
    {
        Report report =
            await _reportRepository.GetByIdAsync(request.ReportId, cancellationToken)
            ?? throw new Exception($"Report with id {request.ReportId} not found.");

        report.Comment(request.Comment);

        return await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}
