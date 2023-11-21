using Domain.AggregateModels.ReportAggregate.Entities;

namespace Domain.AggregateModels.ReportAggregate.Events;

public record EvidenceFromReportRevertedToUnderReviewEvent(Report Report, Evidence Evidence)
    : IDomainEvent;
