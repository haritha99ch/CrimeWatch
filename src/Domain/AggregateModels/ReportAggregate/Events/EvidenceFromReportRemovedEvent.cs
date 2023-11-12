using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record EvidenceFromReportRemovedEvent(Report Report, EvidenceId EvidenceId) : IDomainEvent;
