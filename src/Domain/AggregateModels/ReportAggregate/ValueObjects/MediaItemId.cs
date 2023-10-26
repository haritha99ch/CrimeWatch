namespace Domain.AggregateModels.ReportAggregate.ValueObjects;
public sealed record MediaItemId(Guid Value) : EntityId(Value);
