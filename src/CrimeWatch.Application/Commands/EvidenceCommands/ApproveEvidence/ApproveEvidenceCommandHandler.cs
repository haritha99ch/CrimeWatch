namespace CrimeWatch.Application.Commands.EvidenceCommands.ApproveEvidence;
internal class ApproveEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    : IRequestHandler<ApproveEvidenceCommand, Evidence>
{

    public async Task<Evidence> Handle(ApproveEvidenceCommand request, CancellationToken cancellationToken)
    {
        var evidence =
            await evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
            ?? throw new($"Evidence with id {request.EvidenceId} not found.");

        evidence.Approve();

        return await evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
