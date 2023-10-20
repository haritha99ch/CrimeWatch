namespace CrimeWatch.Application.Commands.EvidenceCommands.RevertEvidenceToReview;
internal class RevertEvidenceToReviewCommandHandler : IRequestHandler<RevertEvidenceToReviewCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public RevertEvidenceToReviewCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<Evidence> Handle(RevertEvidenceToReviewCommand request, CancellationToken cancellationToken)
    {
        var evidence =
            await _evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
            ?? throw new($"Evidence with id {request.EvidenceId} not found.");

        evidence.RevertToReview();

        return await _evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
