using Application.Specifications.Reports;

namespace Application.Features.Reports.Commands.UpdateReport;
internal sealed class UpdateReportCommandHandler : ICommandHandler<UpdateReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public UpdateReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }
    public async Task<Result<Report>> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.GetOneAsync(new ReportIncludingAll(request.ReportId), cancellationToken);
        if (report is null)
            return ReportNotFoundError.Create(
                message: $"Report with ReportID: {request.ReportId.Value.ToString()}, is not found");

        report.Update(
            request.Caption,
            request.Description,
            request.No,
            request.Street1,
            request.Street2,
            request.City,
            request.Province,
            request.ViolationTypes,
            request.MediaItem,
            request.NewMediaItem);

        return await _reportRepository.UpdateAsync(report);
    }
}
