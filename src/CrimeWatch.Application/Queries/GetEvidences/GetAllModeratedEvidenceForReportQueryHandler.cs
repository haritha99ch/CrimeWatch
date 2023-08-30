using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetEvidences;
internal class GetAllModeratedEvidenceForReportQueryHandler : IRequestHandler<GetAllModeratedEvidenceForReportQuery, List<Evidence>>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public GetAllModeratedEvidenceForReportQueryHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<List<Evidence>> Handle(GetAllModeratedEvidenceForReportQuery request, CancellationToken cancellationToken)
        => await _evidenceRepository.GetModeratedEvidencesWithAllForReportAsync(request.ReportId, cancellationToken);
}
