namespace CrimeWatch.Application.Queries.EvidenceQueries.GetAllModeratedEvidenceForReport;
internal class GetAllModeratedEvidenceForReportQueryHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    : IRequestHandler<GetAllModeratedEvidenceForReportQuery,
        List<Evidence>>
{
    public async Task<List<Evidence>> Handle(GetAllModeratedEvidenceForReportQuery request,
        CancellationToken cancellationToken)
        => await evidenceRepository.GetModeratedEvidencesWithAllForReportAsync(request.ReportId, cancellationToken);
}
