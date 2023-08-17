namespace CrimeWatch.Domain.Primitives;
public record Entity<T>(T Id) where T : ValueObject;
