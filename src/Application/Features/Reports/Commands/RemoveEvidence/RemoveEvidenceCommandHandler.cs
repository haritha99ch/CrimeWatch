using Application.Specifications.Reports;
using Persistence.Contracts.Services;

namespace Application.Features.Reports.Commands.RemoveEvidence;
internal sealed class RemoveEvidenceCommandHandler : ICommandHandler<RemoveEvidenceCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IFileStorageService _fileStorageService;

    public RemoveEvidenceCommandHandler(
            IRepository<Report, ReportId> reportRepository,
            IFileStorageService fileStorageService
        )
    {
        _reportRepository = reportRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<bool>> Handle(RemoveEvidenceCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking()
            .GetOneAsync<ReportWithEvidenceById>(
                new(request.ReportId, request.EvidenceId),
                cancellationToken);

        if (report is null) return ReportNotFoundError.Create();
        if (report.Evidences.Count == 0) return EvidenceNotFoundError.Create();

        var mediaItemsToDelete = report.Evidences
            .First(e => e.Id.Equals(request.EvidenceId))
            .MediaItems.Select(e => e.FileName);

        report.RemoveEvidence(report.Evidences.First().Id);
        report = await _reportRepository.UpdateAsync(report, cancellationToken);

        if (report.Evidences.Any(e => e.Id.Equals(request.EvidenceId))) return false;

        foreach (var fileName in mediaItemsToDelete)
        {
            await _fileStorageService.DeleteFileAsync(report.Id.ToString(), fileName, cancellationToken);
        }

        return true;
    }
}
