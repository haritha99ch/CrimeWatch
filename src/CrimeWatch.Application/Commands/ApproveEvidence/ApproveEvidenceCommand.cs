using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ApproveEvidence;
public sealed record ApproveEvidenceCommand(EvidenceId EvidenceId) : IRequest<Evidence>;
