namespace CrimeWatch.Domain.Common;
public class Entity<TValueObject> : BaseEntity where TValueObject : ValueObject
{
    public TValueObject Id { get; set; } = default!;
}
