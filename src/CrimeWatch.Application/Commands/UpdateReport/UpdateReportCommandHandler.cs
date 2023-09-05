using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.UpdateReport;
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
        Report report =
            await _reportRepository.GetReportWithMediaItemByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception("Report not found");


        if (request.MediaItem == null && request.NewMediaItem != null)
        {
            await _fileStorageService.DeleteFileByUrlAsync(report.MediaItem!.Url, report.WitnessId, cancellationToken);
            var (mediaItem, blockBlobClient, blockIds) =
                await _fileStorageService.SaveFileAsync(request.NewMediaItem, report.WitnessId, cancellationToken);

            report.Update(
                request.Title,
                request.Description,
                request.Location,
                mediaItem
            );

            await blockBlobClient.CommitBlockListAsync(blockIds);

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
