using Application.Specifications.Reports;

namespace Application.Features.Reports.Commands.ApproveEvidence;
internal sealed class ApproveEvidenceCommandHandler : ICommandHandler<ApproveEvidenceCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public ApproveEvidenceCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(ApproveEvidenceCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking()
            .GetOneAsync<ReportWithEvidenceById>(
                new(request.ReportId, request.EvidenceId),
                cancellationToken);
        report!.SetApproveEvidence(request.EvidenceId);
        report = await _reportRepository.UpdateAsync(report, cancellationToken);
        return report.Evidences.First(e => e.Id.Equals(request.EvidenceId)).Status.Equals(Status.Approved);
    }
}
