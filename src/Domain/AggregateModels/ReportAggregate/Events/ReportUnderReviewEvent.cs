using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record ReportUnderReviewEvent(Report Report) : IDomainEvent;
