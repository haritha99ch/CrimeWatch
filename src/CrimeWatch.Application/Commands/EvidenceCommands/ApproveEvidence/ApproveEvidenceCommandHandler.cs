using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.EvidenceCommands.ApproveEvidence;
internal class ApproveEvidenceCommandHandler : IRequestHandler<ApproveEvidenceCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public ApproveEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<Evidence> Handle(ApproveEvidenceCommand request, CancellationToken cancellationToken)
    {
        Evidence evidence =
           await _evidenceRepository.GetByIdAsync(request.EvidenceId, cancellationToken)
           ?? throw new Exception($"Evidence with id {request.EvidenceId} not found.");

        evidence.Approve();

        return await _evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
