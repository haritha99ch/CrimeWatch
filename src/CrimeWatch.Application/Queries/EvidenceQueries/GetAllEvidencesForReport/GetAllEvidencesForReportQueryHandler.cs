using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.EvidenceQueries.GetAllEvidencesForReport;
internal sealed class
    GetAllEvidencesForReportQueryHandler : IRequestHandler<GetAllEvidencesForReportQuery, List<Evidence>>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public GetAllEvidencesForReportQueryHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<List<Evidence>> Handle(GetAllEvidencesForReportQuery request, CancellationToken cancellationToken)
        => await _evidenceRepository.GetEvidencesWithAllForReportAsync(request.ReportId, cancellationToken);
}
