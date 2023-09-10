using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.EvidenceCommands.DeclineEvidence;
public sealed record DeclineEvidenceCommand(EvidenceId EvidenceId) : IRequest<Evidence>;
