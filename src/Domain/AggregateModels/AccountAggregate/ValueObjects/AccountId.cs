namespace Domain.AggregateModels.AccountAggregate.ValueObjects;

public record AccountId(Guid Value) : EntityId(Value);
