using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record CommentRemovedFromReportEvent(Report Report, CommentId CommentId) : IDomainEvent;
