namespace Domain.AggregateModels.AccountAggregate.ValueObjects;

public record ModeratorId(Guid Value) : EntityId(Value);
