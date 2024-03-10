using Application.Errors.Files;
using Application.Specifications.Reports;
using Persistence.Contracts.Services;

namespace Application.Features.Reports.Commands.AddEvidence;
internal sealed class AddEvidenceCommandHandler : ICommandHandler<AddEvidenceCommand, EvidenceDetails>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IFileStorageService _fileStorageService;

    public AddEvidenceCommandHandler(
            IRepository<Report, ReportId> reportRepository,
            IFileStorageService fileStorageService
        )
    {
        _reportRepository = reportRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<EvidenceDetails>> Handle(AddEvidenceCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking().GetByIdAsync(request.ReportId, cancellationToken);
        if (report is null) return ReportNotFoundError.Create();

        List<MediaUpload> mediaUploads = [];
        Error? error = default;
        foreach (var mediaItem in request.MediaItems)
        {
            var uploadResult = await _fileStorageService.UploadFileAsync(
                request.ReportId.ToString(),
                mediaItem,
                cancellationToken);
            var isUploadSucceed = uploadResult.Handle(
                e =>
                {
                    mediaUploads.Add(e);
                    return true;
                },
                e =>
                {
                    error = FileUploadError.Create(e.Message, e.InnerException?.Message ?? default);
                    return false;
                }
            );
            if (isUploadSucceed) continue;
            await DeleteUploadsAsync(request.ReportId.ToString(), mediaUploads, cancellationToken);
            return error!;
        }

        var evidence = report.AddEvidence(
            request.AuthorId,
            request.Caption,
            request.Description,
            request.No,
            request.Street1,
            request.Street2,
            request.City,
            request.Province,
            mediaUploads
        );
        await _reportRepository.UpdateAsync(report, cancellationToken);
        var newEvidence = await _reportRepository.GetOneAsync<EvidenceDetailsById, EvidenceDetails>(
            new(request.ReportId, evidence.Id),
            cancellationToken);

        if (newEvidence is null) return EvidenceNotFoundError.Create();
        return newEvidence;
    }
    private async Task DeleteUploadsAsync(
            string containerName,
            IEnumerable<MediaUpload> mediaUploads,
            CancellationToken cancellationToken
        )
    {
        foreach (var mediaUpload in mediaUploads)
        {
            await _fileStorageService.DeleteFileAsync(containerName, mediaUpload.FileName, cancellationToken);
        }
    }
}
