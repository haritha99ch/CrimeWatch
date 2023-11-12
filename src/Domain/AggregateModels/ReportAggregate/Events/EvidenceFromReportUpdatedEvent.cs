using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record EvidenceFromReportUpdatedEvent(Report Report, Evidence Evidence) : IDomainEvent;
