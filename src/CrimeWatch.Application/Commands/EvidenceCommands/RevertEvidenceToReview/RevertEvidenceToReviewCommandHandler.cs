namespace CrimeWatch.Application.Commands.EvidenceCommands.RevertEvidenceToReview;
internal class RevertEvidenceToReviewCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    : IRequestHandler<RevertEvidenceToReviewCommand, Evidence>
{
    public async Task<Evidence> Handle(RevertEvidenceToReviewCommand request, CancellationToken cancellationToken)
    {
        var evidence =
            await evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
            ?? throw new($"Evidence with id {request.EvidenceId} not found.");

        evidence.RevertToReview();

        return await evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
