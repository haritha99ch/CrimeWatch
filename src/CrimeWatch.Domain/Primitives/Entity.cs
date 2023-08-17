namespace CrimeWatch.Domain.Primitives;
public class Entity<T> where T : ValueObject
{
    public T Id { get; set; } = default!;
}
