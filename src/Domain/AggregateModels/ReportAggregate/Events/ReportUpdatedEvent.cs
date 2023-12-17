namespace Domain.AggregateModels.ReportAggregate.Events;
public record ReportUpdatedEvent(Report Report) : IDomainEvent;
