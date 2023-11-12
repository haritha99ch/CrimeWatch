using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record CommentAddedToEvidenceOnReportEvent(Report Report, EvidenceId EvidenceId, Comment Comment) : IDomainEvent;
