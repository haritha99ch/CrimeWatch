namespace CrimeWatch.Application.Commands.EvidenceCommands.DeclineEvidence;
internal class DeclineEvidenceCommandHandler : IRequestHandler<DeclineEvidenceCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public DeclineEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<Evidence> Handle(DeclineEvidenceCommand request, CancellationToken cancellationToken)
    {

        var evidence =
            await _evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
            ?? throw new($"Evidence with id {request.EvidenceId} not found.");

        evidence.Decline();

        return await _evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
