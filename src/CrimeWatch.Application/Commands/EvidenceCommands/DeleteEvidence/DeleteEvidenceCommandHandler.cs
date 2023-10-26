namespace CrimeWatch.Application.Commands.EvidenceCommands.DeleteEvidence;
internal class DeleteEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    : IRequestHandler<DeleteEvidenceCommand, bool>
{

    public async Task<bool> Handle(DeleteEvidenceCommand request, CancellationToken cancellationToken)
        => await evidenceRepository.DeleteByIdAsync(request.Id);
}
