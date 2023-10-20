namespace CrimeWatch.Domain.Primitives;
public class Entity<TValueObject> where TValueObject : ValueObject
{
    public TValueObject Id { get; set; } = default!;
}
