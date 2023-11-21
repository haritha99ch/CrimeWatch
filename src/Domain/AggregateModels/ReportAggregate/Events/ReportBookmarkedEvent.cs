using Domain.AggregateModels.ReportAggregate.Entities;

namespace Domain.AggregateModels.ReportAggregate.Events;

public record ReportBookmarkedEvent(Report Report, Bookmark Bookmark) : IDomainEvent;
