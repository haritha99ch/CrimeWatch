using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record ReportBookmarkRemovedEvent(Report Report, AccountId AccountId) : IDomainEvent;
