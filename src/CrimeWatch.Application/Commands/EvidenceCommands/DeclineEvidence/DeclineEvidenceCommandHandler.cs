namespace CrimeWatch.Application.Commands.EvidenceCommands.DeclineEvidence;
internal class DeclineEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    : IRequestHandler<DeclineEvidenceCommand, Evidence>
{

    public async Task<Evidence> Handle(DeclineEvidenceCommand request, CancellationToken cancellationToken)
    {

        var evidence =
            await evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
            ?? throw new($"Evidence with id {request.EvidenceId} not found.");

        evidence.Decline();

        return await evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
