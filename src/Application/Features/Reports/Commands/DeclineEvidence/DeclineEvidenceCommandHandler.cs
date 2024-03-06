using Application.Specifications.Reports;

namespace Application.Features.Reports.Commands.DeclineEvidence;
internal sealed class DeclineEvidenceCommandHandler : ICommandHandler<DeclineEvidenceCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public DeclineEvidenceCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(DeclineEvidenceCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking()
            .GetOneAsync<ReportWithEvidenceById>(new(request.ReportId, request.EvidenceId), cancellationToken);
        report!.SetDeclineEvidence(request.EvidenceId);
        report = await _reportRepository.UpdateAsync(report, cancellationToken);
        return report.Evidences.First(e => e.Id.Equals(request.EvidenceId)).Status.Equals(Status.Declined);
    }
}
