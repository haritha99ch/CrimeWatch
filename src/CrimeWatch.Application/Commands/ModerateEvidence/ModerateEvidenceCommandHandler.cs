using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ModerateEvidence;
internal class ModerateEvidenceCommandHandler : IRequestHandler<ModerateEvidenceCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public ModerateEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<Evidence> Handle(ModerateEvidenceCommand request, CancellationToken cancellationToken)
    {
        Evidence evidence =
            await _evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
            ?? throw new Exception($"Evidence with id {request.EvidenceId} not found.");

        evidence.Moderate(request.ModeratorId);

        return await _evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
