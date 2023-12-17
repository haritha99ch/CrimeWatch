using Domain.AggregateModels.ReportAggregate.Entities;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record EvidenceFromReportDeclinedEvent(Report Report, Evidence Evidence) : IDomainEvent;
