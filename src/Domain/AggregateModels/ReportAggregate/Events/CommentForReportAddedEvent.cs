using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record CommentForReportAddedEvent(Report Report, Comment Comment) : IDomainEvent;
