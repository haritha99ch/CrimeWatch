using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.ReportCommands.UpdateReport;
internal class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IFileStorageService _fileStorageService;

    public UpdateReportCommandHandler(
        IRepository<Report, ReportId> reportRepository,
        IFileStorageService fileStorageService)
    {
        _reportRepository = reportRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Report> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        var report =
            await _reportRepository.GetReportWithMediaItemByIdAsync(request.Id, cancellationToken)
            ?? throw new("Report not found");


        if (request.MediaItem == null && request.NewMediaItem != null)
        {
            await _fileStorageService.DeleteFileByUrlAsync(report.MediaItem!.Url, report.WitnessId, cancellationToken);
            var mediaItem =
                await _fileStorageService.SaveFileAsync(request.NewMediaItem, report.WitnessId, cancellationToken);

            report.Update(
                request.Title,
                request.Description,
                request.Location,
                mediaItem
            );
        }
        else
        {
            report.Update(
                request.Title,
                request.Description,
                request.Location,
                request.MediaItem!
            );
        }
        return await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}
