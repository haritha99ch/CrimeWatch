using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record ReportRevertedToUnderReviewEvent(Report Report) : IDomainEvent;
