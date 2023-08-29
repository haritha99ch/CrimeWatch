using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.AddCommentToEvidence;
public record class AddCommentToEvidenceCommand
    (EvidenceId EvidenceId, string Comment) : IRequest<Evidence>;
