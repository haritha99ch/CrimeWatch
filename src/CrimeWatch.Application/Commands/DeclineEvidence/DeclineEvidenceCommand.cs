using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.DeclineEvidence;
public sealed record DeclineEvidenceCommand(EvidenceId EvidenceId) : IRequest<Evidence>;
