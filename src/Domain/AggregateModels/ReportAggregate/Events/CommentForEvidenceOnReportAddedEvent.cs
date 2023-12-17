using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record CommentForEvidenceOnReportAddedEvent(
        Report Report,
        EvidenceId EvidenceId,
        Comment Comment
    ) : IDomainEvent;
