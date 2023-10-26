namespace CrimeWatch.Application.Commands.EvidenceCommands.ModerateEvidence;
internal class ModerateEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    : IRequestHandler<ModerateEvidenceCommand, Evidence>
{

    public async Task<Evidence> Handle(ModerateEvidenceCommand request, CancellationToken cancellationToken)
    {
        var evidence =
            await evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
            ?? throw new($"Evidence with id {request.EvidenceId} not found.");

        evidence.Moderate(request.ModeratorId);

        return await evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
