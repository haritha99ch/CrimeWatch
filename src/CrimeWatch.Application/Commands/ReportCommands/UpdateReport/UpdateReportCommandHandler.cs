using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.ReportCommands.UpdateReport;
internal class UpdateReportCommandHandler(
        IRepository<Report, ReportId> reportRepository,
        IFileStorageService fileStorageService)
    : IRequestHandler<UpdateReportCommand, Report>
{

    public async Task<Report> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        var report =
            await reportRepository.GetReportWithMediaItemByIdAsync(request.Id, cancellationToken)
            ?? throw new("Report not found");


        if (request.MediaItem == null && request.NewMediaItem != null)
        {
            await fileStorageService.DeleteFileByUrlAsync(report.MediaItem!.Url, report.WitnessId, cancellationToken);
            var mediaItem =
                await fileStorageService.SaveFileAsync(request.NewMediaItem, report.WitnessId, cancellationToken);

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
        return await reportRepository.UpdateAsync(report, cancellationToken);
    }
}
