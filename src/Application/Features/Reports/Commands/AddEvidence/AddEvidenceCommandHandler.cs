using Application.Specifications.Reports;

namespace Application.Features.Reports.Commands.AddEvidence;
internal sealed class AddEvidenceCommandHandler : ICommandHandler<AddEvidenceCommand, EvidenceDetails>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public AddEvidenceCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<EvidenceDetails>> Handle(AddEvidenceCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.GetByIdAsTrackingAsync(request.ReportId, cancellationToken);
        if (report is null) return ReportNotFoundError.Create();

        var evidence = report.AddEvidence(
            request.AuthorId,
            request.Caption,
            request.Description,
            request.No,
            request.Street1,
            request.Street2,
            request.City,
            request.Province,
            request.MediaItems
        );
        await _reportRepository.UpdateAsync(report, cancellationToken);
        var newEvidence = await _reportRepository.GetOneAsync<EvidenceDetailsById, EvidenceDetails>(
            new(request.ReportId, evidence.Id),
            cancellationToken);

        if (newEvidence is null) return EvidenceNotFoundError.Create();
        return newEvidence;
    }
}
