using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record CommentFromEvidenceOnReportRemovedEvent(
        Report Report,
        EvidenceId EvidenceId,
        CommentId CommentId
    ) : IDomainEvent;
