using Domain.AggregateModels.ReportAggregate.Entities;

namespace Domain.AggregateModels.ReportAggregate.Events;

public record EvidenceAddedForReportEvent(Report Report, Evidence Evidence) : IDomainEvent;
