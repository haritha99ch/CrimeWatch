namespace Domain.AggregateModels.ReportAggregate.Events;

public record ReportApprovedEvent(Report Report) : IDomainEvent;
