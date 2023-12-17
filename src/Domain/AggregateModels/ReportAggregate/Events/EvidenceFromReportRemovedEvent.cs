using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record EvidenceFromReportRemovedEvent(Report Report, EvidenceId EvidenceId) : IDomainEvent;
