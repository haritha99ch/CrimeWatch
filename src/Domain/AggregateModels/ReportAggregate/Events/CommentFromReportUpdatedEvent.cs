using Domain.AggregateModels.ReportAggregate.Entities;

namespace Domain.AggregateModels.ReportAggregate.Events;

public record CommentFromReportUpdatedEvent(Report Report, Comment Comment) : IDomainEvent;
