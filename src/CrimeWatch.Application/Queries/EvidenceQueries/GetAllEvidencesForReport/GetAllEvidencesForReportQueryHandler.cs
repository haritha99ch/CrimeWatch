namespace CrimeWatch.Application.Queries.EvidenceQueries.GetAllEvidencesForReport;
internal sealed class GetAllEvidencesForReportQueryHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    : IRequestHandler<GetAllEvidencesForReportQuery, List<Evidence>>
{

    public async Task<List<Evidence>> Handle(GetAllEvidencesForReportQuery request, CancellationToken cancellationToken)
        => await evidenceRepository.GetEvidencesWithAllForReportAsync(request.ReportId, cancellationToken);
}
