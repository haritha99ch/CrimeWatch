﻿namespace CrimeWatch.Application.Commands.EvidenceCommands.AddCommentToEvidence;
internal class AddCommentToEvidenceCommandHandler : IRequestHandler<AddCommentToEvidenceCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public AddCommentToEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<Evidence> Handle(AddCommentToEvidenceCommand request, CancellationToken cancellationToken)
    {
        var evidence =
            await _evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
            ?? throw new($"Evidence with id {request.EvidenceId} not found.");

        evidence.Comment(request.Comment);

        return await _evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
