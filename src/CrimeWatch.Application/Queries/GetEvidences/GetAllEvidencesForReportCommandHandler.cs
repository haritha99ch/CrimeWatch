using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetEvidences;
internal class GetAllEvidencesForReportCommandHandler : IRequestHandler<GetAllEvidencesForReportCommand, List<Evidence>>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public GetAllEvidencesForReportCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<List<Evidence>> Handle(GetAllEvidencesForReportCommand request, CancellationToken cancellationToken)
        => await _evidenceRepository.GetEvidencesWithAllForReportAsync(request.ReportId, cancellationToken);
}
