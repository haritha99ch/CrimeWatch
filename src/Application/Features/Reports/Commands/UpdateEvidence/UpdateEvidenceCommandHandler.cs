using Application.Specifications.Reports;

namespace Application.Features.Reports.Commands.UpdateEvidence;
internal sealed class UpdateEvidenceCommandHandler : ICommandHandler<UpdateEvidenceCommand, EvidenceDetails>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public UpdateEvidenceCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
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
            request.NewMediaItems);

        await _reportRepository.UpdateAsync(report, cancellationToken);
        var evidence = await _reportRepository.GetOneAsync<EvidenceDetailsById, EvidenceDetails>(
            new(request.ReportId, request.EvidenceId),
            cancellationToken);

        return evidence!;
    }
}
