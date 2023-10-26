namespace CrimeWatch.Application.Commands.EvidenceCommands.AddCommentToEvidence;
public class AddCommentToEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    : IRequestHandler<AddCommentToEvidenceCommand, Evidence>
{

    public async Task<Evidence> Handle(AddCommentToEvidenceCommand request, CancellationToken cancellationToken)
    {
        var evidence =
            await evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
            ?? throw new($"Evidence with id {request.EvidenceId} not found.");

        evidence.Comment(request.Comment);

        return await evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
