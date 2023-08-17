namespace CrimeWatch.Domain.Primitives;
public record AggregateRoot<T>(T Id) : Entity<T>(Id) where T : ValueObject;
