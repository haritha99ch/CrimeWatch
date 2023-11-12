using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record EvidenceOnReportDeclinedEvent(Report Report, Evidence Evidence) : IDomainEvent;
