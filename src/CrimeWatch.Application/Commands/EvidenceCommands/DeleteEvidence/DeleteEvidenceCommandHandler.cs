﻿namespace CrimeWatch.Application.Commands.EvidenceCommands.DeleteEvidence;
internal class DeleteEvidenceCommandHandler : IRequestHandler<DeleteEvidenceCommand, bool>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public DeleteEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<bool> Handle(DeleteEvidenceCommand request, CancellationToken cancellationToken)
        => await _evidenceRepository.DeleteByIdAsync(request.Id);
}
