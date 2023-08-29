using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ModerateEvidence;
public sealed record ModerateEvidenceCommand(EvidenceId EvidenceId, ModeratorId ModeratorId) : IRequest<Evidence>;
