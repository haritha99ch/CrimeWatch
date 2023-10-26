namespace CrimeWatch.Domain.Common;
public abstract class Entity<TValueObject> : BaseEntity where TValueObject : ValueObject
{
    public TValueObject Id { get; set; } = default!;
}
