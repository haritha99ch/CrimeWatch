using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record CommentFromReportRemovedEvent(Report Report, CommentId CommentId) : IDomainEvent;
