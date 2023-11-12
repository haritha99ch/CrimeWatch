using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record CommentFromReportRemovedEvent(Report Report, CommentId CommentId) : IDomainEvent;
