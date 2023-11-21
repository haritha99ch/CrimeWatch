namespace Domain.AggregateModels.ReportAggregate.Events;

public record ReportCreatedEvent(Report Report) : IDomainEvent;
