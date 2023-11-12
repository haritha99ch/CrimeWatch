using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record ReportBookmarkedEvent(Report Report, Bookmark Bookmark) : IDomainEvent;
