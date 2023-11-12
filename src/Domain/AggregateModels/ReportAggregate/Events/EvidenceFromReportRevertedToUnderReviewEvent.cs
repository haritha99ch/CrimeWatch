using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.Contracts.Models;

namespace Domain.AggregateModels.ReportAggregate.Events;
public record EvidenceFromReportRevertedToUnderReviewEvent(Report Report, Evidence Evidence) : IDomainEvent;
