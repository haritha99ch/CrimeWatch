using Domain.AggregateModels.ReportAggregate.Entities;

namespace Domain.AggregateModels.ReportAggregate.Events;

public record CommentForReportAddedEvent(Report Report, Comment Comment) : IDomainEvent;
