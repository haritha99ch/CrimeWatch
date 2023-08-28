using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.UpdateEvidence;
internal class UpdateEvidenceCommandHandler : IRequestHandler<UpdateEvidenceCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public UpdateEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<Evidence> Handle(UpdateEvidenceCommand request, CancellationToken cancellationToken)
    {
        Evidence evidence =
            await _evidenceRepository.GetByAsync<EvidenceByIdWithMediaItems>(new(request.Id), cancellationToken)
            ?? throw new Exception("Evidence not found");

        evidence.Update(
            request.Caption,
            request.Description,
            request.Location,
            request.MediaItems ?? new()
        );

        return await _evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
