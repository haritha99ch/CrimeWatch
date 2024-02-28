using Application.Specifications.Reports;

namespace Application.Features.Reports.Commands.RemoveEvidence;
internal sealed class RemoveEvidenceCommandHandler : ICommandHandler<RemoveEvidenceCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public RemoveEvidenceCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(RemoveEvidenceCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking()
            .GetOneAsync<ReportWithEvidenceById>(
                new(request.ReportId, request.EvidenceId),
                cancellationToken);

        if (report is null) return ReportNotFoundError.Create();
        if (report.Evidences.Count == 0) return EvidenceNotFoundError.Create();

        report.RemoveEvidence(report.Evidences.First().Id);
        await _reportRepository.UpdateAsync(report, cancellationToken);
        return true;
    }
}
