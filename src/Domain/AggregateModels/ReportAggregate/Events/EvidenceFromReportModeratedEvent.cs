using Domain.AggregateModels.ReportAggregate.Entities;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record EvidenceFromReportModeratedEvent(Report Report, Evidence Evidence) : IDomainEvent;
