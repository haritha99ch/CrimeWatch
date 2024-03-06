using Application.Specifications.Reports;

namespace Application.Features.Reports.Commands.RevertEvidenceToUnderReview;
internal sealed class RevertEvidenceToUnderReviewCommandHandler
    : ICommandHandler<RevertEvidenceToUnderReviewCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public RevertEvidenceToUnderReviewCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(
            RevertEvidenceToUnderReviewCommand request,
            CancellationToken cancellationToken
        )
    {
        var report = await _reportRepository.AsTracking()
            .GetOneAsync<ReportWithEvidenceById>(new(request.ReportId, request.EvidenceId), cancellationToken);
        report!.SetUnderReviewEvidence(request.EvidenceId);
        report = await _reportRepository.UpdateAsync(report, cancellationToken);
        return report.Evidences.First(e => e.Id.Equals(request.EvidenceId)).Status.Equals(Status.UnderReview);
    }
}
