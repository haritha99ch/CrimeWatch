using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.EvidenceCommands.ModerateEvidence;
public sealed record ModerateEvidenceCommand(EvidenceId EvidenceId, ModeratorId ModeratorId) : IRequest<Evidence>;
