using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record ReportBookmarkRemovedEvent(Report Report, AccountId AccountId) : IDomainEvent;
