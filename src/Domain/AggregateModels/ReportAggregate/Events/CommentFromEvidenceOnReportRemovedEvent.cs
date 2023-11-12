using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record CommentFromEvidenceOnReportRemovedEvent(Report Report, EvidenceId EvidenceId, CommentId CommentId)
    : IDomainEvent;
