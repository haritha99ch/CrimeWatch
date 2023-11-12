using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record EvidenceAddedForReportEvent(Report Report, Evidence Evidence) : IDomainEvent;
