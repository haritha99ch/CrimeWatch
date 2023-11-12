using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record ReportModeratedEvent(Report Report) : IDomainEvent;
