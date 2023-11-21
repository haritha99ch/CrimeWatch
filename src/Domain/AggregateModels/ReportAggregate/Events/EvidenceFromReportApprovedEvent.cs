using Domain.AggregateModels.ReportAggregate.Entities;

namespace Domain.AggregateModels.ReportAggregate.Events;

public record EvidenceFromReportApprovedEvent(Report Report, Evidence Evidence) : IDomainEvent;
