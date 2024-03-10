using Application.Errors.Files;
using Application.Specifications.Reports;
using Persistence.Contracts.Services;

namespace Application.Features.Reports.Commands.UpdateEvidence;
internal sealed class UpdateEvidenceCommandHandler : ICommandHandler<UpdateEvidenceCommand, EvidenceDetails>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IFileStorageService _fileStorageService;

    public UpdateEvidenceCommandHandler(
            IRepository<Report, ReportId> reportRepository,
            IFileStorageService fileStorageService
        )
    {
        _reportRepository = reportRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<EvidenceDetails>> Handle(
            UpdateEvidenceCommand request,
            CancellationToken cancellationToken
        )
    {
        var report = await _reportRepository.AsTracking()
            .GetOneAsync<ReportWithEvidenceById>(
                new(request.ReportId, request.EvidenceId, true),
                cancellationToken);
        if (report is null)
            return ReportNotFoundError.Create(message: "Report is not found associated with the evidence.");
        if (report.Evidences.Count == 0) return EvidenceNotFoundError.Create("Evidence is not found to update.");

        List<MediaUpload> newMediaUploads = [];
        Error? error = default;
        foreach (var mediaItem in request.NewMediaItems ?? [])
        {
            var uploadResult = await _fileStorageService.UploadFileAsync(
                request.ReportId.ToString(),
                mediaItem,
                cancellationToken);
            var isUploadSucceed = uploadResult.Handle(
                e =>
                {
                    newMediaUploads.Add(e);
                    return true;
                },
                e =>
                {
                    error = FileUploadError.Create(e.Message, e.InnerException?.Message ?? default);
                    return false;
                }
            );
            if (isUploadSucceed) continue;
            await DeleteUploadsAsync(request.ReportId.ToString(),
                newMediaUploads.Select(e => e.FileName),
                cancellationToken);
            return error!;
        }

        var requestFileNames = request.MediaItems?.Select(mi => mi.FileName).ToHashSet() ?? [];
        var mediaItemsToDelete = report.Evidences.First(e => e.Id.Equals(request.EvidenceId))
            .MediaItems.Where(m => !requestFileNames.Contains(m.FileName))
            .Select(e => e.FileName);
        await DeleteUploadsAsync(
            request.ReportId.ToString(),
            mediaItemsToDelete,
            cancellationToken);

        report.UpdateEvidence(
            request.EvidenceId,
            request.Caption,
            request.Description,
            request.No,
            request.Street1,
            request.Street2,
            request.City,
            request.Province,
            request.MediaItems,
            newMediaUploads);

        await _reportRepository.UpdateAsync(report, cancellationToken);
        var evidence = await _reportRepository.GetOneAsync<EvidenceDetailsById, EvidenceDetails>(
            new(request.ReportId, request.EvidenceId),
            cancellationToken);

        return evidence!;
    }

    private async Task DeleteUploadsAsync(
            string containerName,
            IEnumerable<string> mediaUploadFileNames,
            CancellationToken cancellationToken
        )
    {
        foreach (var mediaUploadFileName in mediaUploadFileNames)
        {
            await _fileStorageService.DeleteFileAsync(containerName, mediaUploadFileName, cancellationToken);
        }
    }
}
