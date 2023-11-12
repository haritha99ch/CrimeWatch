using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record ReportDeclinedEvent(Report Report) : IDomainEvent;
