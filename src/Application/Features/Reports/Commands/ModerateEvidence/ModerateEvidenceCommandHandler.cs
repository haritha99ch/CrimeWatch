using Application.Specifications.Reports;

namespace Application.Features.Reports.Commands.ModerateEvidence;
internal sealed class ModerateEvidenceCommandHandler : ICommandHandler<ModerateEvidenceCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public ModerateEvidenceCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(ModerateEvidenceCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking()
            .GetOneAsync<ReportWithEvidenceById>(new(request.ReportId, request.EvidenceId), cancellationToken);
        report!.SetModeratorModeratorForEvidence(request.EvidenceId, request.AccountId);
        report = await _reportRepository.UpdateAsync(report, cancellationToken);
        return report.Evidences.First(e => e.Id.Equals(request.EvidenceId)).Status.Equals(Status.UnderReview);
    }
}
