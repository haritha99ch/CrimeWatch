using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.UpdateReport;
internal class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public UpdateReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        Report report =
            await _reportRepository.GetReportWithMediaItemByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception("Report not found");

        report.Update(
            request.Title,
            request.Description,
            request.Location,
            request.MediaItem
        );

        return await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}
