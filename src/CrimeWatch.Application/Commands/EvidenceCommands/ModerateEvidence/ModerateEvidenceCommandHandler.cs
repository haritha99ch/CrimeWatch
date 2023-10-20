namespace CrimeWatch.Application.Commands.EvidenceCommands.ModerateEvidence;
internal class ModerateEvidenceCommandHandler : IRequestHandler<ModerateEvidenceCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public ModerateEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<Evidence> Handle(ModerateEvidenceCommand request, CancellationToken cancellationToken)
    {
        var evidence =
            await _evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
            ?? throw new($"Evidence with id {request.EvidenceId} not found.");

        evidence.Moderate(request.ModeratorId);

        return await _evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
