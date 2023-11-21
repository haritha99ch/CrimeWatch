using Domain.AggregateModels.ReportAggregate.Entities;

namespace Domain.AggregateModels.ReportAggregate.Events;

public record EvidenceFromReportUpdatedEvent(Report Report, Evidence Evidence) : IDomainEvent;
