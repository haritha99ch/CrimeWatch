namespace Domain.AggregateModels.AccountAggregate.Common;
public abstract record VerificationCode(int Value, DateTime CreatedAt)
{
    public bool IsExpired => CreatedAt.AddMinutes(5) < DateTime.Now;

    protected VerificationCode(int value)
        : this(value, DateTime.Now) { }

    public virtual bool Equals(VerificationCode? other)
    {
        if (other is null) return false;
        if (GetType() != other.GetType()) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => HashCode.Combine(Value, CreatedAt);
}
