namespace Domain.AggregateModels.ReportAggregate.ValueObjects;

public sealed record EvidenceId(Guid Value) : EntityId(Value);
